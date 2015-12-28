var Config = require('./../../config');

var _ = require('underscore');
var EventEmitter = require('events');
var S = require('string');

var lolapi = require('lolapi')(Config.league.apikey, Config.league.location);
var leagueDb = require('./util/league-db');

var gameMode = require('./util/gametype-constant');
var getSummonerIdFunction = require('./util/get-summoner-id');

var lolCurrentGameInfo = function(bot, user, userID, channelID, message) {
    if (S(message).contains("!lolcurrentgame")) {
        bot.simulateTyping(channelID, function() {
            var splitMessage = message.split('=');
            var summonerName = splitMessage[1];
            
            var getSummonerId = new EventEmitter();
            getSummonerIdFunction(_, lolapi, leagueDb, userID, getSummonerId, summonerName);
            
            getSummonerId.on('fail', function(message) {
            bot.sendMessage({
                    to: channelID,
                    message: message
                }); 
            });
            
            getSummonerId.on('completed', function(summonerID) {
                lolapi.CurrentGame.getBySummonerId(summonerID, function(error, game) {
                    if (error) {
                        bot.sendMessage({
                            to: channelID,
                            message: 'ERROR: ' + error + '. Are you in a game?'
                        });
                    } else {
                        var gameType = gameMode(game);
                        
                        _.each(game.participants, function(participant) {
                            if (participant.summonerId === summonerID) {
                                    lolapi.Static.getChampion(participant.championId, function(error, champion) {
                                        var player = _.isString(summonerName) ? summonerName : user;
                                        bot.sendMessage({
                                            to: channelID,
                                            message: player + " is currently playing " + champion.name + ' in a ' + gameType + ' Game.'
                                        });
                                    });
                            }
                        });
                    }
                });
            });
        });
    }
};

module.exports = lolCurrentGameInfo;