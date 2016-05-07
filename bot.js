import { Config } from './config';

import Discordie from 'discordie';
const client = new Discordie();

// Modules
import * as util from './modules/utils';

import league from './modules/lol/index';

import sound from './modules/sound/index';
import soundFileupload from './modules/sound/modules/fileupload';

import management from './modules/management/index';

import poll from './modules/poll/index';

import remindme from './modules/remindme/index';
import remindmeCommand from './modules/remindme/command';

// import lastseen from './modules/lastseen/index';
// import lastseenCommand from './modules/lastseen/command';

import twitter from './modules/twitter/index';

import { chunder } from './modules/silly';

export function bot() {
	client.connect({
		token: Config.discord.token,
	});

	client.Dispatcher.on('GATEWAY_READY', () => {
		console.log('Connected as: ' + client.User.id + ' - ' + client.User.username); // eslint-disable-line

		client.User.setGame({
			name: '@disbott help for cmd!',
		});

		// Start the logging functions
		remindme(client);
		// lastseen(client);
	});

	client.Dispatcher.on('MESSAGE_CREATE', e => {
		const wasMentioned = client.User.isMentioned(e.message);

		if (wasMentioned && e.message.author.id !== client.User.id) {
			const message = e.message.content.replace(`<@${client.User.id}> `, '');

			try {
				util.ping(e, message);
				util.help(e, message);
				util.about(e, message);
				util.info(e, message);
				util.deleteMessages(e, message);

				// kill(client, channelID, message);

				chunder(e, message);

				league(e, message);

				sound(client, e, message);

				management(e, message);

				poll(e, message);

				remindmeCommand(e, message);

				// lastseenCommand(client, user, userID, channelID, message);

				twitter(e, message);
			} catch (error) {
				console.log(error); // eslint-disable-line
			}
		}

		// soundFileupload is a little different to other commands so it has to be put here
		// Currently it'll accept any mp3 messaged to it
		soundFileupload(e);
	});

	client.Dispatcher.on('VOICE_CHANNEL_JOIN', e => {
		util.userHasJoinedVoiceChannel(e);
	});
}

export function botDisconnect() {
	client.disconnect();
}

bot();
