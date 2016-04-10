import disconnect from './modules/disconnect';
import joinVoiceChannel from './modules/join-voice-channel';
import listSounds from './modules/list-sounds';
import playSound from './modules/play-sound';

export default function sound(client, e, message) {
	if (!client.VoiceConnections.length) {
		if (message === 'vjoin') {
			joinVoiceChannel(e, message);
		} else {
			return e.message.reply('Not connected to any channel');
		}
	} else {
		if (message === 'vjoin') {
			return e.message.reply('disbott is already connected');
		}

		var stopPlaying = false;

		if (message === 'vstop') {
			stopPlaying = true;
		}

		disconnect(client, e, message);
		playSound(client, e, message, stopPlaying);
	}

	listSounds(e, message);
};
