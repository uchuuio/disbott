import { Config } from './config';

import _ from 'underscore';
import S from 'string';
import DiscordClient from 'discord.io';

// Modules
import { about, help, info, ping } from './modules/utils';

import league from './modules/lol/index';

import sound from './modules/sound/index';
import soundFileupload from './modules/sound/modules/fileupload';

import management from './modules/management/index';

import remindme from './modules/remindme/index';
import remindmeCommand from './modules/remindme/command';

import lastseen from './modules/lastseen/index';
import lastseenCommand from './modules/lastseen/command';

import twitter from './modules/twitter/index';

import { chunder } from './modules/silly';

var bot = new DiscordClient({
	token: Config.discord.token,
});

bot.sendMessages = function (ID, messageArr, interval) {
	var callback = [];
	var resArr = [];
	var len = messageArr.length;
	typeof (arguments[2]) === 'function' ? callback = arguments[2] : callback = arguments[3];
	if (typeof (interval) !== 'number') interval = 1000;

	function _sendMessages() {
		setTimeout(function () {
			if (messageArr[0]) {
				bot.sendMessage({
					to: ID,
					message: messageArr.shift(),
				}, function (res) {
					resArr.push(res);
					if (resArr.length === len) if (typeof (callback) === 'function') callback(resArr);
				});

				_sendMessages();
			}
		}, interval);
	}

	_sendMessages();
};

bot.connect();

bot.on('ready', function () {
	console.log(bot.username + ' - (' + bot.id + ')');
	bot.setPresence({
		game: 'Hacking Simulator 2k16',
	});

	// Start the logging function
	remindme(bot);
	lastseen(bot);
});

bot.on('message', function (user, userID, channelID, message, rawEvent) {
	var wasMentioned = _.findWhere(rawEvent.d.mentions, { id: bot.id });

	if (wasMentioned && userID !== bot.id) {
		message = S(message).chompLeft('<@' + bot.id + '> ').s;

		try {
			ping(bot, channelID, message);
			help(Config, bot, channelID, message);
			about(Config, bot, channelID, message);
			info(Config, bot, channelID, message);

			// kill(bot, channelID, message);

			chunder(bot, channelID, message);

			league(bot, user, userID, channelID, message);

			sound(Config, bot, channelID, message, rawEvent);

			management(Config, bot, channelID, message, rawEvent);

			remindmeCommand(bot, user, userID, channelID, message);
			lastseenCommand(bot, user, userID, channelID, message);

			twitter(bot, channelID, message);
		} catch (e) {
			console.log(e);
		}
	}

	// soundFileupload is a little different to other commands so it has to be put here
	// Currently it'll accept any mp3, I'd like it to only be mp3's dm'd to it
	soundFileupload(bot, channelID, rawEvent);
});
