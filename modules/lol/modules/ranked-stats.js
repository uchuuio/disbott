import { Config } from './../../../config';

import _ from 'underscore';
var EventEmitter = require('events');
import S from 'string';

var lolapi = require('lolapi')(Config.league.apikey, Config.league.location);
import { leagueDb } from './util/league-db';

import getSummonerIdFunction from './util/get-summoner-id';

export default function rankedStats(bot, user, userID, channelID, message) {
	if (S(message).contains('lolrankedstats')) {
		bot.simulateTyping(channelID, function () {
			var splitMessage = message.split('=');
			var summonerName = splitMessage[1];

			var getSummonerId = new EventEmitter();
			getSummonerIdFunction(S, lolapi, leagueDb, userID, getSummonerId, summonerName);

			getSummonerId.on('fail', function (message) {
				bot.sendMessage({
					to: channelID,
					message: message,
				});
			});

			getSummonerId.on('completed', function (summonerID) {
				lolapi.Stats.getRanked(summonerID, null, function (err, stats) {
					var player = _.isString(summonerName) ? summonerName : user;
					if (err || stats === null) {
						bot.sendMessage({
							to: channelID,
							message: player + ' is not ranked this season',
						});
					} else {
						var combinedStatsIndex = _.findIndex(stats.champions, { id: 0 });
						var combinedStats = stats.champions[combinedStatsIndex];
						var winloss = combinedStats.stats.totalSessionsWon + ' wins & ' + combinedStats.stats.totalSessionsLost + ' losses';

						var K = combinedStats.stats.totalChampionKills;
						var A = combinedStats.stats.totalAssists;
						var D = combinedStats.stats.totalDeathsPerSession;
						var kda = S((K + (A / 4)) / D).toFloat(3) + ' kda';

						lolapi.League.getEntriesBySummonerId(summonerID, function (err, leagues) {
							if (err || leagues === null) {
								bot.sendMessage({
									to: channelID,
									message: player + ' is not ranked this season',
								});
							} else {
								var leaguesIndex = _.findIndex(leagues[summonerID], {queue: 'RANKED_SOLO_5x5'});
								var league = leagues[summonerID][leaguesIndex];
								var rank = league.tier + ' ' + league.entries[0].division;
								bot.sendMessages(channelID, [
									player + ' is ranked in ' + rank + ' with ' + winloss + ',',
									K + 'kills ' + D + 'deaths ' + A + 'assists with a ' + kda + ' which equates to ' + S(K / D).toFloat(3) + 'KD this season.',
								]);
							}
						});
					}
				});
			});
		});
	}
};
