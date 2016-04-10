import { Config } from './../../../config';

import _ from 'underscore';
var EventEmitter = require('events');
import S from 'string';

var lolapi = require('lolapi')(Config.league.apikey, Config.league.location);
import { leagueDb } from './util/league-db';

import gameMode from './util/gametype-constant';
import getSummonerIdFunction from './util/get-summoner-id';

export default function lolGetSetSummoner(e, message) {
	if (S(message).contains('lolsummoner')) {
		e.message.channel.sendTyping();

		var splitMessage = message.split('=');
		var summonerName = splitMessage[1];
		var userID = e.message.author.id;

		var player = _.isString(summonerName) ? summonerName : e.message.author.mention;

		var getSummonerId = new EventEmitter();
		getSummonerIdFunction(S, lolapi, leagueDb, userID, getSummonerId, summonerName);

		getSummonerId.on('fail', function (message) {
			e.message.channel.sendMessage(message);
		});

		getSummonerId.on('completed', function (summonerID) {
			lolapi.Summoner.get(summonerID, function (error, summoner) {
				if (error) {
					e.message.channel.sendMessage('ERROR: Could not get summoner');
				} else {
					e.message.channel.sendMessage(`${player}\'s league account is ${summoner[summonerID].name}`);
				}
			});
		});
	}
};
