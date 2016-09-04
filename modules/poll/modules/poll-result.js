export default function pollResult(pollDb, e) {
	const time = 60 * 1000;
	setTimeout(() => {
		pollDb.find({ enabled: true }, (err, result) => {
			if (result.length !== 0) {
				const poll = result[0];
				const yTotal = poll.result.y;
				const nTotal = poll.result.n;
				const totalVotes = yTotal + nTotal;
				const yPercent = (yTotal / totalVotes) * 100;
				const nPercent = (nTotal / totalVotes) * 100;

				e.message.channel.sendMessage(`The poll: ${poll.topic} Results are in:`);
				e.message.channel.sendMessage(`Yes: ${yTotal} - ${yPercent}%`);
				e.message.channel.sendMessage(`No: ${nTotal} - ${nPercent}%`);
				if (yTotal > nTotal) {
					e.message.channel.sendMessage('The yeses have it!');
				} else {
					e.message.channel.sendMessage('The noes have it!');
				}

				pollDb.remove({}, { multi: true });
			} else {
				e.message.message.channel.sendMessage('There was no poll');
			}
		});
	}, time);
}
