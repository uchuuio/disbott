export function gazo(T, e, message) {
	if (message === 'gazo') {
		T.get('statuses/user_timeline', {
			screen_name: 'idol_gazo',
			count: 50,
		}, (err, data) => {
			const tweet = data[Math.floor(Math.random() * data.length)];
			const person = tweet.text.split(' #')[0];
			const image = tweet.entities.media[0].media_url_https;

			const gazoMessage = `${person}"\r\n"${image}`;

			e.message.channel.sendMessage(gazoMessage);
		});
	}
}
