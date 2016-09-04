import { Config } from './../../../config';

import _ from 'underscore';
const EventEmitter = require('events');
import S from 'string';
const s = S;
import moment from 'moment';

const lolapi = require('lolapi')(Config.league.apikey, Config.league.location);
import { leagueDb } from './util/league-db';

import gameMode from './util/gametype-constant';
import getSummonerIdFunction from './util/get-summoner-id';

export default function lolCurrentGameInfo(e, message) {
	if (s(message).contains('lolcurrentgame')) {
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
			lolapi.CurrentGame.getBySummonerId(summonerID, (error, game) => {
				if (error) {
					e.message.channel.sendMessage(`ERROR: ${error}. Are you in a game?`);
				} else {
					const gameType = gameMode(game);

					const currentLength = moment(game.gameStartTime).toNow(true);

					_.each(game.participants, (participant) => {
						if (participant.summonerId === summonerID) {
							lolapi.Static.getChampion(participant.championId, (err, champion) => {
								const player = _.isString(summonerName) ? summonerName : e.message.author.mention;

								e.message.channel.sendMessage(`${player} has been playing ${champion.name} in a ${gameType} Game for ~${currentLength}. ${Config.domain}/currentgame.html?summonerID=' + summonerID`);
							});
						}
					});
				}
			});
		});
	}
}
