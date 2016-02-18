require('dotenv').load();

var Config = {
	discord: {
		email: process.env.DISCORD_EMAIL,
		password: process.env.DISCORD_PASSWORD
	},
	league: {
		apikey: process.env.LEAGUE_APIKEY,
		location: process.env.LEAGUE_LOCATION
	},
	twitter: {
		consumer_key: process.env.TWITTER_CONSUMER_KEY,
		consumer_secret: process.env.TWITTER_CONSUMER_SECRET,
		access_token: process.env.TWITTER_ACCESS_TOKEN,
		access_token_secret: process.env.TWITTER_ACCESS_TOKEN_SECRET
	},
	domain: 'https://disbott.pagu.co'
};

module.exports = Config;
