export default function votePoll(pollDb, e, message) {
	if (message.indexOf('votepoll') > -1) {
		const splitMessage = message.split('=');
		const vote = splitMessage[1];

		pollDb.find({ enabled: true }, (err, result) => {
			if (result.length !== 0) {
				const poll = result[0];
				const pollId = poll._id;
				if (vote === 'y') {
					const newYTotal = poll.result.y + 1;
					pollDb.update(
						{ _id: pollId },
						{ $set: { result: {
							y: newYTotal,
							n: poll.result.n,
						} } },
						{ multi: true }
					);
				} else if (vote === 'n') {
					const newNTotal = poll.result.n + 1;
					pollDb.update(
						{ _id: pollId },
						{ $set: { result: {
							y: poll.result.y,
							n: newNTotal,
						} } },
						{ multi: true }
					);
				} else {
					e.message.channel.sendMessage('Please only use vote=y or vote=n');
				}
			} else {
				e.message.channel.sendMessage('There isn\'t a poll in progress');
			}
		});
	}
}
