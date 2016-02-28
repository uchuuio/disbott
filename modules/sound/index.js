var Datastore = require('nedb');
var soundDb = new Datastore({
	filename: './datastores/sound.db',
	autoload: true
});

var S = require('string');

var disconnect = require('./modules/disconnect');
var joinVoiceChannel = require('./modules/join-voice-channel');
var stopSound = require('./modules/stopsound');
var listSounds = require('./modules/list-sounds');
var playSound = require('./modules/play-sound');

var sound = function (Config, bot, channelID, message, rawEvent) {
	soundDb.find({ enabled: true }, function (err, data) {
		if (data.length > 0) {
			var voiceChannelID = data[0].voiceChannelID;
			stopSound(bot, channelID, message, voiceChannelID);
			disconnect(soundDb, bot, channelID, message, voiceChannelID);
			playSound(S, bot, channelID, message, voiceChannelID);
		} else if (S(message).contains('sound=')) {
			joinVoiceChannel(Config, soundDb, bot, channelID, message);
		}
	});

	listSounds(Config, bot, channelID, message);
};

module.exports = sound;
