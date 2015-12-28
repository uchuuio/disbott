var Config = require('./../../config');

var S = require('string');
var lolapi = require('lolapi')(Config.league.apikey, Config.league.location);
var leagueDb = require('./util/league-db');

var lolSetSummoner = function(bot, user, userID, channelID, message) {
    if (S(message).contains("!lolsetsummoner")) {
        bot.simulateTyping(channelID, function() {
            var splitMessage = message.split('=');
            var summonerName = splitMessage[1];

            lolapi.Summoner.getByName(summonerName, function (error, summoner) {
                if (error) throw error;

                var summonerId = summoner[summonerName].id;
                var data = {
                    discordUserId: userID,
                    leagueSummonerId: summonerId
                };
                
                leagueDb.insert(data, function(err, newData) {
                    bot.sendMessage({
                        to: channelID,
                        message: user + ' linked ' + summonerName + ' to disbot.'
                    });
                });
            });
        });
    }
}

module.exports = lolSetSummoner;