import { Config } from './config';

var fs = require('fs');

var lolapi = require('lolapi')(Config.league.apikey, Config.league.location);
import _ from 'underscore';
var EventEmitter = require('events');
import moment from 'moment';
import gameMode from './modules/lol/modules/util/gametype-constant';

var express = require('express');
var app = express();

app.use(express.static('web'));

app.get('/api/lol/current-game', function (req, res) {
	var summonerID = req.query.summonerID;
	lolapi.CurrentGame.getBySummonerId(summonerID, function (error, game) {
		if (error) {
			res.status(404).json({ error: 'No Game Found' });
			console.log(error);
		} else {
			game.gameType = gameMode(game);
			game.gameLength = moment(game.gameStartTime).toNow(true);

			var getChampions = new EventEmitter();
			var count = (game.participants.length - 1);
			_.each(game.participants, function (participant, i) {
				lolapi.Static.getChampion(participant.championId, function (error, champion) {
					game.participants[i].champion = champion.name;

					// Only send the json when we've set all the champions
					if (count === i) {
						getChampions.emit('completed', game);
					}
				});
			});

			getChampions.on('completed', function (game) {
				_.groupBy(game.participants, 'teamId');
				res.json(game);
			});
		}
	});
});

app.get('/api/sounds', function (req, res) {
	fs.readdir('./modules/sound/sounds/', function (err, files) {
		var sounds = _.without(files, '.DS_Store', '.gitignore');
		res.json(sounds);
	});
});

var server = app.listen(5000, function () {
	var host = server.address().address;
	var port = server.address().port;

	console.log('Example app listening at http://%s:%s', host, port);
});
