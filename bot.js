import { Config } from './config';

import _ from 'underscore';
import S from 'string';

import Discordie from 'discordie';
var client = new Discordie();

// Modules
import { about, help, info, ping } from './modules/utils';

import league from './modules/lol/index';

import sound from './modules/sound/index';
import soundFileupload from './modules/sound/modules/fileupload';

import management from './modules/management/index';

import remindme from './modules/remindme/index';
import remindmeCommand from './modules/remindme/command';

// import lastseen from './modules/lastseen/index';
// import lastseenCommand from './modules/lastseen/command';

import twitter from './modules/twitter/index';

import { chunder } from './modules/silly';

client.connect({
	token: Config.discord.token,
});

client.Dispatcher.on('GATEWAY_READY', e => {
	console.log('Connected as: ' + client.User.id + ' - ' + client.User.username);

	client.User.setGame({
		name: 'Hacking Simulator 2k16',
	});

	// Start the logging functions
	remindme(client);
	// lastseen(client);
});

client.Dispatcher.on('MESSAGE_CREATE', e => {
	var wasMentioned = client.User.isMentioned(e.message);

	if (wasMentioned && e.message.author.id !== client.User.id) {
		var message = S(e.message.content).chompLeft('<@' + client.User.id + '> ').s;

		try {
			ping(e, message);
			help(e, message);
			about(e, message);
			info(e, message);

			// kill(client, channelID, message);

			chunder(e, message);

			league(e, message);

			sound(client, e, message);

			management(e, message);

			remindmeCommand(e, message);

			// lastseenCommand(client, user, userID, channelID, message);

			twitter(e, message);
		} catch (error) {
			console.log(error);
		}
	}

	// soundFileupload is a little different to other commands so it has to be put here
	// Currently it'll accept any mp3 messaged to it
	soundFileupload(e);
});
