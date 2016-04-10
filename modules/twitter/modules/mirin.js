import moment from 'moment';

export function mirin(T, e, message) {
	if (message === 'mirin') {
		T.get('statuses/user_timeline', {
			screen_name: 'FurukawaMirin',
			count: 1,
		}, function (err, data) {
			var tweet = data[0];

			var message = `Mirin last said:\r\n${tweet.text}\r\nApprox. ${moment(tweet.created_at).fromNow()} - https://twitter.com/FurukawaMirin/status/${tweet.id}`;

			e.message.channel.sendMessage(message);
		});
	}
};
