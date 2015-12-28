var Config = require('./../../config');

var _ = require('underscore');
var EventEmitter = require('events');
var S = require('string');

var lolapi = require('lolapi')(Config.league.apikey, Config.league.location);
var leagueDb = require('./util/league-db');

var getSummonerIdFunction = require('./util/get-summoner-id');

var rankedStats = function(bot, user, userID, channelID, message) {
    if (S(message).contains("!lolrankedstats")) {
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
                lolapi.Stats.getRanked(summonerID, null, function(err, stats) {
                    var combinedStatsIndex = _.findIndex(stats.champions, {id: 0});
                    var combinedStats = stats.champions[combinedStatsIndex];
                    var player = _.isString(summonerName) ? summonerName : user;
                    var winloss = combinedStats.stats.totalSessionsWon+'w'+combinedStats.stats.totalSessionsLost+'l';
                    
                    var K = combinedStats.stats.totalChampionKills;
                    var A = combinedStats.stats.totalAssists;
                    var D = combinedStats.stats.totalDeathsPerSession;
                    var kda = (K+A)/D + 'kda';
                    
                    lolapi.League.getEntriesBySummonerId(summonerID, function(err, leagues) {
                        var leaguesIndex = _.findIndex(leagues[summonerID], {queue: "RANKED_SOLO_5x5"});
                        var league = leagues[summonerID][leaguesIndex];
                        var rank = league.tier + ' ' + league.entries[0].division;
                        bot.sendMessage({
                            to: channelID,
                            message: player + ': ' + rank + ' / ' + winloss + ' / ' + kda + ' this season'
                        }); 
                    })

                });
            });
        });
    }
};

module.exports = rankedStats;