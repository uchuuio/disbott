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

var league = require('./modules/lol/index');

var sound = require('./modules/sound/index');
var soundFileupload = require('./modules/sound/modules/fileupload');

var bot = new DiscordClient({
    email: Config.discord.email,
    password: Config.discord.password
});

bot.sendMessages = function (ID, messageArr, interval) {
	var callback, resArr = [], len = messageArr.length;
	typeof(arguments[2]) === 'function' ? callback = arguments[2] : callback = arguments[3];
	if (typeof(interval) !== 'number') interval = 1000;
	
	function _sendMessages() {
		setTimeout(function() {
			if (messageArr[0]) {
				bot.sendMessage({
					to: ID,
					message: messageArr.shift()
				}, function(res) {
					resArr.push(res);
					if (resArr.length === len) if (typeof(callback) === 'function') callback(resArr);
				});
				_sendMessages();
			}
		}, interval);
	}
	_sendMessages();
}

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
        
        league(bot, user, userID, channelID, message);
        
        sound(Config, bot, channelID, message, rawEvent);
    }

    // soundFileupload is a little different to other commands so it has to be put here
    // Currently it'll accept any mp3, I'd like it to only be mp3's dm'd to it
    soundFileupload(bot, channelID, rawEvent);
});