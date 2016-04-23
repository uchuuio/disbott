import moment from 'moment';

export function headline(T, e, message) {
	if (message === 'headline') {
		T.get('statuses/user_timeline', {
			screen_name: 'guardian',
			count: 1,
		}, (err, data) => {
			const tweet = data[0];
			const headlineMessage = `Via. The Guardian\r\n${tweet.text}\r\nPosted approx. ${moment(tweet.created_at).fromNow()}`;

			e.message.channel.sendMessage(headlineMessage);
		});
	}
}
