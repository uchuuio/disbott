var Config = require('./../../config');

var _ = require('underscore');
var EventEmitter = require('events');
var S = require('string');

var lolapi = require('lolapi')(Config.league.apikey, Config.league.location);
var leagueDb = require('./util/league-db');

var gameMode = require('./util/gametype-constant');
var getSummonerIdFunction = require('./util/get-summoner-id');

var lolGetSetSummoner = function(bot, user, userID, channelID, message) {
    if (S(message).contains("!lolsummoner")) {
        bot.simulateTyping(channelID, function() {
            var splitMessage = message.split('=');
            var summonerName = splitMessage[1];
            
            var player = _.isString(summonerName) ? summonerName : user;
            
            var getSummonerId = new EventEmitter();
            getSummonerIdFunction(S, lolapi, leagueDb, userID, getSummonerId, summonerName);
            
            getSummonerId.on('fail', function(message) {
            bot.sendMessage({
                    to: channelID,
                    message: message
                }); 
            });
            
            getSummonerId.on('completed', function(summonerID) {
                lolapi.Summoner.get(summonerID, function(error, summoner) {
                    if (error) {
                        bot.sendMessage({
                            to: channelID,
                            message: 'ERROR: Couldn'
                        });
                    } else {
                        bot.sendMessage({
                            to: channelID,
                            message: player + '\'s league account is ' + summoner[summonerID].name
                        });
                    }
                });
            });
        });
    }
};

module.exports = lolGetSetSummoner;