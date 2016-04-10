import { Config } from './../../../config';

import S from 'string';
var lolapi = require('lolapi')(Config.league.apikey, Config.league.location);
import { leagueDb } from './util/league-db';

export default function lolSetSummoner(e, message) {
	if (S(message).contains('lolsetsummoner')) {
		e.message.channel.sendTyping();

		var splitMessage = message.split('=');
		var summonerName = splitMessage[1];
		var userID = e.message.author.id;

		lolapi.Summoner.getByName(summonerName, function (error, summoner) {
			try {
				if (error) { throw error; }

				var summonerId = summoner[summonerName].id;
				var data = {
					discordUserId: userID,
					leagueSummonerId: summonerId,
				};
				console.log();

				if (leagueDb.find({
					discordUserId: userID,
				}, function (err, summoner) {
					if (S(summoner).isEmpty()) {
						leagueDb.insert(data, function (err, newData) {
							e.message.channel.sendMessage(`${e.message.author.mention} set ${summonerName} as their league account for disbott.`);
						});
					} else {
						leagueDb.update(
							{ discordUserId: userID },
							{ $set: { leagueSummonerId: summonerId } },
							{},
							function (err, numReplaced) {
								e.message.channel.sendMessage(`${e.message.author.mention} set ${summonerName} as their updated league account for disbott.`);
							}
						);
					}
				}));
			} catch (error) {
				console.log(error);
				e.message.channel.sendMessage('There was an error, are you sure you spelt the summonername right?');
			}
		});
	}
};
