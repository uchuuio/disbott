import joinVoiceChannel from './modules/join-voice-channel';
import listSounds from './modules/list-sounds';
import playSound from './modules/play-sound';

export default function sound(client, e, message) {
	if (!client.VoiceConnections.length) {
		if (message === 'vjoin') {
			joinVoiceChannel(e, message);
			return;
		}
	} else {
		if (message === 'vjoin') {
			e.message.reply('disbott is already connected');
		}

		let stopPlaying = false;

		if (message === 'vstop' || message === 'vleave') {
			stopPlaying = true;
		}

		playSound(client, e, message, stopPlaying);

		return;
	}

	listSounds(e, message);
}
