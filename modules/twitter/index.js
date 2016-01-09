var Config = require('./../../config');

var Twit = require('twit');
var T = new Twit({
    consumer_key: Config.twitter.consumer_key,
    consumer_secret: Config.twitter.consumer_secret,
    access_token: Config.twitter.access_token,
    access_token_secret: Config.twitter.access_token_secret
});

var mirin = require('./modules/mirin');
var gazo = require('./modules/gazo');
var headline = require('./modules/headline');

var twitter = function (bot, channelID, message) {
    mirin(T, bot, channelID, message);
    headline(T, bot, channelID, message);
    gazo(T, bot, channelID, message);
}

module.exports = twitter;