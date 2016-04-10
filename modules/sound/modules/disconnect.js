export default function disconnect(client, e, message) {
	if (message === 'vleave') {
		client.Channels
			.filter(channel => channel.type == 'voice')
			.forEach(channel => {
				if (channel.joined) {
					channel.leave();
				}
			});

		e.message.channel.sendMessage('Disconnected!');
	}
};
