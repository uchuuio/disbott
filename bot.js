var Config = require('./config.js');

var _ = require('underscore');
var S = require('string');
var DiscordClient = require('discord.io');

// Modules
var ping = require('./modules/ping');
var help = require('./modules/help');
var about = require('./modules/about');
var info = require('./modules/info');
var kill = require('./modules/kill');

var lolSetSummoner = require('./modules/lol/set-summoner');
var lolGetSetSummoner = require('./modules/lol/get-set-summoner');
var lolCurrentGameInfo = require('./modules/lol/current-game');
var lolRankedStats = require('./modules/lol/ranked-stats');

var sound = require('./modules/sound/index');
var soundFileupload = require('./modules/sound/modules/fileupload');

var bot = new DiscordClient({
    email: Config.discord.email,
    password: Config.discord.password
});

bot.connect();

bot.on('ready', function() {
    console.log(bot.username + " - (" + bot.id + ")");
    bot.setPresence({
        game: "Hacking Simulator 2k16"
    });
});

bot.on('message', function(user, userID, channelID, message, rawEvent) {
    var wasMentioned = _.findWhere(rawEvent.d.mentions, {id: bot.id});

    if (wasMentioned && userID !== bot.id) {
        message = S(message).chompLeft('<@' + bot.id + '> ').s;

        ping(bot, channelID, message);
        help(Config, bot, channelID, message);
        about(Config, bot, channelID, message);
        info(Config, bot, channelID, message);
        kill(bot, channelID, message);
        
        lolSetSummoner(bot, user, userID, channelID, message);
        lolGetSetSummoner(bot, user, userID, channelID, message);
        lolCurrentGameInfo(bot, user, userID, channelID, message);
        lolRankedStats(bot, user, userID, channelID, message);
        
        sound(Config, bot, channelID, message, rawEvent);
    }

    // soundFileupload is a little different to other commands so it has to be put here
    // Currently it'll accept any mp3, I'd like it to only be mp3's dm'd to it
    soundFileupload(bot, channelID, rawEvent);
});