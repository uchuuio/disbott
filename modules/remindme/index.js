import { remindmeDb } from './remindmeDb';

import _ from 'underscore';
import moment from 'moment';

export default function remindme(client) {
	setInterval(function () {
		remindmeDb.find({ remindTime: moment().format() }, function (err, reminders) {
			if (reminders.length > 0) {
				_.each(reminders, function (reminder) {
					var setTime = moment(reminder.setTime).fromNow();

					var user = client.Users.find(u => u.id == reminder.userID);
					user.openDM().then(function (channel) {
						channel.sendMessage(`Hi, this is disbott reminding you: ${reminder.remindMessage}; from ${setTime}`);
					});

				});
			}
		});
	}, 1000);
};
