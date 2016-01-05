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
    domain: 'https://disbott.pagu.co'
};

module.exports = Config;