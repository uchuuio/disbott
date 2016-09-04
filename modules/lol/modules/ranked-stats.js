import { Config } from './../../../config';

import _ from 'underscore';
const EventEmitter = require('events');
import S from 'string';
const s = S;

const lolapi = require('lolapi')(Config.league.apikey, Config.league.location);
import { leagueDb } from './util/league-db';

import getSummonerIdFunction from './util/get-summoner-id';

export default function rankedStats(e, message) {
	if (s(message).contains('lolrankedstats')) {
		e.message.channel.sendTyping();

		const splitMessage = message.split('=');
		const summonerName = splitMessage[1];
		const userID = e.message.author.id;

		const getSummonerId = new EventEmitter();
		getSummonerIdFunction(S, lolapi, leagueDb, userID, getSummonerId, summonerName);

		getSummonerId.on('fail', (errMessage) => {
			e.message.channel.sendMessage(errMessage);
		});

		getSummonerId.on('completed', (summonerID) => {
			lolapi.Stats.getRanked(summonerID, null, (err, stats) => {
				const player = _.isString(summonerName) ? summonerName : e.message.author.mention;
				if (err || stats === null) {
					e.message.channel.sendMessage(`${player} is not ranked this season`);
				} else {
					const combinedStatsIndex = _.findIndex(stats.champions, { id: 0 });
					const combinedStats = stats.champions[combinedStatsIndex];
					const winloss = `${combinedStats.stats.totalSessionsWon} wins & ${combinedStats.stats.totalSessionsLost} losses`;

					const K = combinedStats.stats.totalChampionKills;
					const A = combinedStats.stats.totalAssists;
					const D = combinedStats.stats.totalDeathsPerSession;
					const kda = `${s((K + (A / 4)) / D).toFloat(3)} kda`;

					lolapi.League.getEntriesBySummonerId(summonerID, (error, leagues) => {
						if (error || leagues === null) {
							e.message.channel.sendMessage(`${player} is not ranked this season`);
						} else {
							const leaguesIndex = _.findIndex(leagues[summonerID], { queue: 'RANKED_SOLO_5x5' });
							const league = leagues[summonerID][leaguesIndex];
							const rank = `${league.tier} ${league.entries[0].division}`;

							let rankMessage = `${player} is ranked in ${rank} with ${winloss}\r\n`;
							rankMessage += `${K}kills ${D}deaths ${A}assists with a ${kda} which equates to ${s(K / D).toFloat(3)}KD this season.`;

							e.message.channel.sendMessage(rankMessage);
						}
					});
				}
			});
		});
	}
}
