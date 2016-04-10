import { remindmeDb } from './remindmeDb';

import S from 'string';
import moment from 'moment';

export default function remindmeCommand(e, message) {
	if (S(message).contains('remindme=')) {
		var splitMessage = message.split('=');
		var timeAndMessage = splitMessage[1].split(';');

		var timeTo = timeAndMessage[0].split('.');
		var countTimeToArray = timeTo.length / 2;
		var currentTime = moment();
		var time = moment();

		for (var i = 0; i < countTimeToArray; i++) {
			var amountOfTime = timeTo[i];
			var unitOfTime = timeTo[i + 1];
			time = time.add(amountOfTime, unitOfTime);
			i++;
		}

		var setTime = currentTime.format();
		var remindTime = time.format();
		var timeToNice = time.fromNow();

		var remindMessage = timeAndMessage[1];

		var data = {
			userID: e.message.author.id,
			setTime: setTime,
			remindTime: remindTime,
			remindMessage: remindMessage,
		};

		remindmeDb.insert(data, function (err, newDoc) {
			e.message.channel.sendMessage(`I will remind you (${e.message.author.mention}) ${timeToNice}: ${remindMessage}`);
		});
	}
};
