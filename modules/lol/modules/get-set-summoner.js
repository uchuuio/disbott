import { Config } from './../../../config';

import _ from 'underscore';
var EventEmitter = require('events');
import S from 'string';

var lolapi = require('lolapi')(Config.league.apikey, Config.league.location);
import { leagueDb } from './util/league-db';

import gameMode from './util/gametype-constant';
import getSummonerIdFunction from './util/get-summoner-id';

export default function lolGetSetSummoner(bot, user, userID, channelID, message) {
	if (S(message).contains('lolsummoner')) {
		bot.simulateTyping(channelID, function () {
			var splitMessage = message.split('=');
			var summonerName = splitMessage[1];

			var player = _.isString(summonerName) ? summonerName : user;

			var getSummonerId = new EventEmitter();
			getSummonerIdFunction(S, lolapi, leagueDb, userID, getSummonerId, summonerName);

			getSummonerId.on('fail', function (message) {
				bot.sendMessage({
					to: channelID,
					message: message,
				});
			});

			getSummonerId.on('completed', function (summonerID) {
				lolapi.Summoner.get(summonerID, function (error, summoner) {
					if (error) {
						bot.sendMessage({
							to: channelID,
							message: 'ERROR: Could not get summoner',
						});
					} else {
						bot.sendMessage({
							to: channelID,
							message: player + '\'s league account is ' + summoner[summonerID].name,
						});
					}
				});
			});
		});
	}
};
