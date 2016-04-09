import S from 'string';

export function gazo(T, bot, channelID, message) {
	if (message === 'gazo') {
		T.get('statuses/user_timeline', {
			screen_name: 'idol_gazo',
			count: 50,
		}, function (err, data) {
			var tweet = data[Math.floor(Math.random() * data.length)];
			var person = tweet.text.split(' #')[0];
			var image = tweet.entities.media[0].media_url_https;

			bot.sendMessages(channelID, [
				person,
				image,
			]);
		});
	}
};
