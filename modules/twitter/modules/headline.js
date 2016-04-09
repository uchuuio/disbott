import moment from 'moment';

export function headline(T, bot, channelID, message) {
	if (message === 'headline') {
		T.get('statuses/user_timeline', {
			screen_name: 'guardian',
			count: 1,
		}, function (err, data) {
			var tweet = data[0];
			bot.sendMessages(channelID, [
				'Via. The Guardian',
				tweet.text,
				'Posted approx. ' + moment(tweet.created_at).fromNow(),
			]);
		});
	}
};
