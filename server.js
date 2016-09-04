import { Config } from './config';

const fs = require('fs');

const lolapi = require('lolapi')(Config.league.apikey, Config.league.location);
import _ from 'underscore';
const EventEmitter = require('events');
import moment from 'moment';
import gameMode from './modules/lol/modules/util/gametype-constant';

const express = require('express');
const app = express();

app.use(express.static('web'));

app.get('/api/lol/current-game', (req, res) => {
	const summonerID = req.query.summonerID;
	lolapi.CurrentGame.getBySummonerId(summonerID, (error, currentGameInfo) => {
		if (error) {
			res.status(404).json({ error: 'No Game Found' });
		} else {
			const game = currentGameInfo;
			game.gameType = gameMode(game);
			game.gameLength = moment(game.gameStartTime).toNow(true);

			const getChampions = new EventEmitter();
			const count = (game.participants.length - 1);
			_.each(game.participants, (participant, i) => {
				lolapi.Static.getChampion(participant.championId, (err, champion) => {
					game.participants[i].champion = champion.name;

					// Only send the json when we've set all the champions
					if (count === i) {
						getChampions.emit('completed', game);
					}
				});
			});

			getChampions.on('completed', (currentGame) => {
				_.groupBy(currentGame.participants, 'teamId');
				res.json(currentGame);
			});
		}
	});
});

app.get('/api/sounds', (req, res) => {
	fs.readdir('./modules/sound/sounds/', (err, files) => {
		const sounds = _.without(files, '.DS_Store', '.gitignore', 'ENV');
		res.json(sounds);
	});
});

const server = app.listen(5000, () => {
	const host = server.address().address;
	const port = server.address().port;

	console.log('Disbott Web listening at http://%s:%s', host, port); // eslint-disable-line
});
