import { remindmeDb } from './remindmeDb';

import S from 'string';
const s = S;
import moment from 'moment';

export default function remindmeCommand(e, message) {
	if (s(message).contains('remindme=')) {
		const splitMessage = message.split('=');
		const timeAndMessage = splitMessage[1].split(';');

		const timeTo = timeAndMessage[0].split('.');
		const countTimeToArray = timeTo.length / 2;
		const currentTime = moment();
		let time = moment();

		for (let i = 0; i < countTimeToArray; i++) {
			const amountOfTime = timeTo[i];
			const unitOfTime = timeTo[i + 1];
			time = time.add(amountOfTime, unitOfTime);
			i++;
		}

		const setTime = currentTime.format();
		const remindTime = time.format();
		const timeToNice = time.fromNow();

		const remindMessage = timeAndMessage[1];

		const data = {
			userID: e.message.author.id,
			setTime,
			remindTime,
			remindMessage,
		};

		remindmeDb.insert(data, (err) => {
			if (err) { throw err; }
			e.message.channel.sendMessage(`I will remind you (${e.message.author.mention}) ${timeToNice}: ${remindMessage}`);
		});
	}
}
