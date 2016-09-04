import { remindmeDb } from './remindmeDb';

import _ from 'underscore';
import moment from 'moment';

export default function remindme(client) {
	setInterval(() => {
		remindmeDb.find({ remindTime: moment().format() }, (err, reminders) => {
			if (reminders.length > 0) {
				_.each(reminders, (reminder) => {
					const setTime = moment(reminder.setTime).fromNow();

					const user = client.Users.find(u => u.id === reminder.userID);
					user.openDM().then((channel) => {
						channel.sendMessage(`Hi, this is disbott reminding you: ${reminder.remindMessage}; from ${setTime}`);
					});
				});
			}
		});
	}, 1000);
}
