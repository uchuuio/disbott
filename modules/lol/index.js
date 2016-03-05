var lolSetSummoner = require('./modules/set-summoner');
var lolGetSetSummoner = require('./modules/get-set-summoner');
var lolCurrentGameInfo = require('./modules/current-game');
var lolRankedStats = require('./modules/ranked-stats');

var league = function (bot, user, userID, channelID, message) {
	message = message.toLowerCase();
	lolSetSummoner(bot, user, userID, channelID, message);
	lolGetSetSummoner(bot, user, userID, channelID, message);
	lolCurrentGameInfo(bot, user, userID, channelID, message);
	lolRankedStats(bot, user, userID, channelID, message);
};

module.exports = league;
