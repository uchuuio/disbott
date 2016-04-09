import { Config } from './../../../config';

import _ from 'underscore';
var EventEmitter = require('events');
import S from 'string';
import moment from 'moment';

var lolapi = require('lolapi')(Config.league.apikey, Config.league.location);
import { leagueDb } from './util/league-db';

import gameMode from './util/gametype-constant';
import getSummonerIdFunction from './util/get-summoner-id';

export default function lolCurrentGameInfo(bot, user, userID, channelID, message) {
	if (S(message).contains('lolcurrentgame')) {
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
				lolapi.CurrentGame.getBySummonerId(summonerID, function (error, game) {
					if (error) {
						bot.sendMessage({
							to: channelID,
							message: 'ERROR: ' + error + '. Are you in a game?',
						});
					} else {
						var gameType = gameMode(game);

						var currentLength = moment(game.gameStartTime).toNow(true);

						_.each(game.participants, function (participant) {
							if (participant.summonerId === summonerID) {
								lolapi.Static.getChampion(participant.championId, function (error, champion) {
									var player = _.isString(summonerName) ? summonerName : user;
									bot.sendMessage({
										to: channelID,
										message: player + ' has been playing ' + champion.name + ' in a ' + gameType + ' Game for ~' + currentLength + '. ' + Config.domain + '/currentgame.html?summonerID=' + summonerID,
									});
								});
							}
						});
					}
				});
			});
		});
	}
};
