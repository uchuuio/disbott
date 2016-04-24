require('dotenv').config();

export const Config = {
	discord: {
		token: process.env.DISCORD_TOKEN,
		testerToken: process.env.DISBOTT_TESTER_TOKEN,
	},
	league: {
		apikey: process.env.LEAGUE_APIKEY,
		location: process.env.LEAGUE_LOCATION,
	},
	twitter: {
		consumer_key: process.env.TWITTER_CONSUMER_KEY,
		consumer_secret: process.env.TWITTER_CONSUMER_SECRET,
		access_token: process.env.TWITTER_ACCESS_TOKEN,
		access_token_secret: process.env.TWITTER_ACCESS_TOKEN_SECRET,
	},
	domain: 'https://disbott.pagu.co',
};
