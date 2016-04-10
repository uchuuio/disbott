import S from 'string';

export function gazo(T, e, message) {
	if (message === 'gazo') {
		T.get('statuses/user_timeline', {
			screen_name: 'idol_gazo',
			count: 50,
		}, function (err, data) {
			var tweet = data[Math.floor(Math.random() * data.length)];
			var person = tweet.text.split(' #')[0];
			var image = tweet.entities.media[0].media_url_https;

			var message = person + '\r\n' + image;

			e.message.channel.sendMessage(message);
		});
	}
};
