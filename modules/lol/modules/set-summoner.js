import { Config } from './../../../config';

import S from 'string';
const s = S;
const lolapi = require('lolapi')(Config.league.apikey, Config.league.location);
import { leagueDb } from './util/league-db';

export default function lolSetSummoner(e, message) {
	if (s(message).contains('lolsetsummoner')) {
		e.message.channel.sendTyping();

		const splitMessage = message.split('=');
		const summonerName = splitMessage[1];
		const userID = e.message.author.id;

		lolapi.Summoner.getByName(summonerName, (error, apisummoner) => {
			try {
				const summonerId = apisummoner[summonerName].id;
				const data = {
					discordUserId: userID,
					leagueSummonerId: summonerId,
				};

				leagueDb.find({
					discordUserId: userID,
				}, (err, summoner) => {
					if (s(summoner).isEmpty()) {
						leagueDb.insert(data, (errorInsert) => {
							if (errorInsert) { throw errorInsert; }
							e.message.channel.sendMessage(`${e.message.author.mention} set ${summonerName} as their league account for disbott.`);
						});
					} else {
						leagueDb.update(
							{ discordUserId: userID },
							{ $set: { leagueSummonerId: summonerId } },
							{},
							(errorUpdate) => {
								if (errorUpdate) { throw errorUpdate; }
								e.message.channel.sendMessage(`${e.message.author.mention} set ${summonerName} as their updated league account for disbott.`);
							}
						);
					}
				});
			} catch (err) {
				e.message.channel.sendMessage('There was an error, are you sure you spelt the summonername right?');
			}
		});
	}
}
