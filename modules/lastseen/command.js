import { lastseenDb } from './lastseenDb';

import S from 'string';
import moment from 'moment';

export default function lastseenCommand(bot, user, userID, channelID, message) {
	if (S(message).contains('lastseen=')) {
		var splitMessage = message.split('=');

		var username = splitMessage[1];
		userID = S(username).chompLeft('<@').s;
		userID = S(userID).chompRight('>').s;

		if (lastseenDb.findOne({
			discordUserId: userID,
		}, function (err, discordUser) {

			if (discordUser.status === 'online') {
				bot.sendMessage({
					to: channelID,
					message: username + ' is currently online',
				});
			} else if (!discordUser.lastseen) {
				bot.sendMessage({
					to: channelID,
					message: username + ' has not been online yet, so I don\'t know when they were last online',
				});
			} else {
				var lastseen = moment(discordUser.lastseen).fromNow();
				bot.sendMessage({
					to: channelID,
					message: username + ' was last seen ' + lastseen,
				});
			}

		}));
	}
};
