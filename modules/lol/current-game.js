var Config = require('./../../config');

var _ = require('underscore');
var EventEmitter = require('events');
var S = require('string');

var lolapi = require('lolapi')(Config.league.apikey, Config.league.location);
var leagueDb = require('./util/league-db');

var gameMode = require('./util/gametype-constant');

var lolCurrentGameInfo = function(bot, user, userID, channelID, message) {
    if (S(message).contains("!lolcurrentgame")) {
        var splitMessage = message.split('=');
        var summonerName = splitMessage[1];
        
        var getSummonerId = new EventEmitter();
        
        if (_.isString(summonerName)) {
            lolapi.Summoner.getByName(summonerName, function (error, summoner) {
                if (error) {
                    bot.sendMessage({
                        to: channelID,
                        message: 'ERROR: Couldn\'t find a summoner with that name on EUW'
                    });
                } else {
                    var summonerId = summoner[summonerName].id;
                    getSummonerId.emit('completed', summonerId);
                }
            });
        } else {
            if (leagueDb.find({
                discordUserId: userID
            }, function(err, summoner) {
                if (_.isEmpty(summoner)) {
                    bot.sendMessage({
                        to: channelID,
                        message: 'ERROR: Have you linked your league account with !lolsetsummoner=[yoursummonername]?'
                    });
                } else {
                    var summonerID = summoner[0].leagueSummonerId;
                    getSummonerId.emit('completed', summonerID);
                }
            }));
        };
        
        
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

    }
};

module.exports = lolCurrentGameInfo;