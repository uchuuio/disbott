import { Config } from './../../../config';

import _ from 'underscore';
const EventEmitter = require('events');
import S from 'string';
const s = S;

const lolapi = require('lolapi')(Config.league.apikey, Config.league.location);
import { leagueDb } from './util/league-db';

import getSummonerIdFunction from './util/get-summoner-id';

export default function lolGetSetSummoner(e, message) {
	if (s(message).contains('lolsummoner')) {
		e.message.channel.sendTyping();

		const splitMessage = message.split('=');
		const summonerName = splitMessage[1];
		const userID = e.message.author.id;

		const player = _.isString(summonerName) ? summonerName : e.message.author.mention;

		const getSummonerId = new EventEmitter();
		getSummonerIdFunction(S, lolapi, leagueDb, userID, getSummonerId, summonerName);

		getSummonerId.on('fail', (errMessage) => {
			e.message.channel.sendMessage(errMessage);
		});

		getSummonerId.on('completed', (summonerID) => {
			lolapi.Summoner.get(summonerID, (error, summoner) => {
				if (error) {
					e.message.channel.sendMessage('ERROR: Could not get summoner');
				} else {
					e.message.channel.sendMessage(`${player}\'s league account is ${summoner[summonerID].name}`);
				}
			});
		});
	}
}
