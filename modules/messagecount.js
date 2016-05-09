import Datastore from 'nedb';
export const messagecountDb = new Datastore({
	filename: './datastores/messagecount.db',
	autoload: true,
});

import S from 'string';
const string = S;

export function messagecountCommand(e, message) {
	if (string(message).contains('messagecount=')) {
		const splitMessage = message.split('= ');

		const username = splitMessage[1];
		let userID = string(username).chompLeft('<@').s;
		userID = string(userID).chompRight('>').s;

		messagecountDb.findOne({
			userID,
		}, (err, discordUser) => {
			e.message.reply(`<@${userID}> has made ${discordUser.messagecount} posts`);
		});
	}
}

export function messageCountLog(e) {
	const authorID = e.message.author.id;
	messagecountDb.findOne({ userID: authorID }, (err, user) => {
		if (user === null) {
			// New User
			messagecountDb.insert({
				userID: authorID,
				messagecount: 1,
			});
		} else {
			// Update User
			const oldMessagecount = user.messagecount;
			const newMessagecount = oldMessagecount + 1;
			messagecountDb.update({
				userID: authorID,
			}, {
				$set: { messagecount: newMessagecount },
			});
		}
	});
}
