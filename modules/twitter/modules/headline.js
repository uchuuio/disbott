import moment from 'moment';

export function headline(T, e, message) {
	if (message === 'headline') {
		T.get('statuses/user_timeline', {
			screen_name: 'guardian',
			count: 1,
		}, function (err, data) {
			var tweet = data[0];
			var message = `Via. The Guardian\r\n${tweet.text}\r\nPosted approx. ${moment(tweet.created_at).fromNow()}`;

			e.message.channel.sendMessage(message);
		});
	}
};
