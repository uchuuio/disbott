import moment from 'moment';

export function mirin(T, e, message) {
	if (message === 'mirin') {
		T.get('statuses/user_timeline', {
			screen_name: 'FurukawaMirin',
			count: 1,
		}, (err, data) => {
			const tweet = data[0];

			const mirinMessage = `Mirin last said:\r\n${tweet.text}\r\nApprox. ${moment(tweet.created_at).fromNow()} - https://twitter.com/FurukawaMirin/status/${tweet.id}`;

			e.message.channel.sendMessage(mirinMessage);
		});
	}
}
