var Config = require('./config.js');

var DiscordClient = require('discord.io');

// Modules
var ping = require('./modules/ping');
var help = require('./modules/help');
var kill = require('./modules/kill');

var lolSetSummoner = require('./modules/lol/set-summoner');
var lolGetSetSummoner = require('./modules/lol/get-set-summoner');
var lolCurrentGameInfo = require('./modules/lol/current-game');
var lolRankedStats = require('./modules/lol/ranked-stats');

var sound = require('./modules/sound/index');

var bot = new DiscordClient({
    email: Config.discord.email,
    password: Config.discord.password,
    autorun: true
});


bot.on('ready', function() {
    console.log(bot.username + " - (" + bot.id + ")");
    bot.setPresence({
        game: "Hacking Simulator 2k16"
    });
});

bot.on('message', function(user, userID, channelID, message, rawEvent) {
    if (userID !== bot.id) {    
        ping(bot, channelID, message);
        help(bot, channelID, message);
        kill(bot, channelID, message);
        
        lolSetSummoner(bot, user, userID, channelID, message);
        lolGetSetSummoner(bot, user, userID, channelID, message);
        lolCurrentGameInfo(bot, user, userID, channelID, message);
        lolRankedStats(bot, user, userID, channelID, message);
        
        sound(bot, channelID, message, rawEvent);
    }
});