import Datastore from 'nedb';
var soundDb = new Datastore({
	filename: './datastores/sound.db',
	autoload: true,
});

import S from 'string';

import disconnect from './modules/disconnect';
import joinVoiceChannel from './modules/join-voice-channel';
import stopSound from './modules/stopsound';
import listSounds from './modules/list-sounds';
import playSound from './modules/play-sound';

export default function sound(Config, bot, channelID, message, rawEvent) {
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
