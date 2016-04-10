import { Config } from './../../../config';

import _ from 'underscore';
var EventEmitter = require('events');
import S from 'string';
import moment from 'moment';

var lolapi = require('lolapi')(Config.league.apikey, Config.league.location);
import { leagueDb } from './util/league-db';

import gameMode from './util/gametype-constant';
import getSummonerIdFunction from './util/get-summoner-id';

export default function lolCurrentGameInfo(e, message) {
	if (S(message).contains('lolcurrentgame')) {
		e.message.channel.sendTyping();

		var splitMessage = message.split('=');
		var summonerName = splitMessage[1];
		var userID = e.message.author.id;

		var getSummonerId = new EventEmitter();
		getSummonerIdFunction(S, lolapi, leagueDb, userID, getSummonerId, summonerName);

		getSummonerId.on('fail', function (message) {
			e.message.channel.sendMessage(message);
		});

		getSummonerId.on('completed', function (summonerID) {
			lolapi.CurrentGame.getBySummonerId(summonerID, function (error, game) {
				if (error) {
					e.message.channel.sendMessage('ERROR: ' + error + '. Are you in a game?');
				} else {
					var gameType = gameMode(game);

					var currentLength = moment(game.gameStartTime).toNow(true);

					_.each(game.participants, function (participant) {
						if (participant.summonerId === summonerID) {
							lolapi.Static.getChampion(participant.championId, function (error, champion) {
								var player = _.isString(summonerName) ? summonerName : e.message.author.mention;

								e.message.channel.sendMessage(`${player} has been playing ${champion.name} in a ${gameType} Game for ~${currentLength}. ${Config.domain}/currentgame.html?summonerID=' + summonerID`);
							});
						}
					});
				}
			});
		});
	}
};
