import pollResult from './poll-result.js';

export default function startPoll(pollDb, e, message) {
	if (message.indexOf('startpoll') > -1) {
		const splitMessage = message.split('=');
		const topic = splitMessage[1];

		pollDb.find({ enabled: true }, (err, result) => {
			if (result.length === 0) {
				pollDb.insert({
					enabled: true,
					topic,
					result: {
						y: 0,
						n: 0,
					},
				}, () => {
					pollResult(pollDb, e);

					e.message.channel.sendMessage(`Starting Poll for 60s on: ${topic}`);
				});
			} else {
				e.message.channel.sendMessage('There is already a poll in progress');
			}
		});
	}
}
