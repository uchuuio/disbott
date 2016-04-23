// const fs = require('fs');
import S from 'string';
const s = S;

export default function playSound(client, e, message) {
	const guild = e.message.channel.guild;

	function stop() {
		const info = client.VoiceConnections.getForGuild(guild);
		if (info) {
			const encoderStream = info.voiceConnection.getEncoderStream();
			encoderStream.unpipeAll();
		}
	}

	function play(info, song) {
		let newInfo = info;
		if (!client.VoiceConnections.length) {
			return console.log('Voice not connected'); // eslint-disable-line
		}

		if (!info) newInfo = client.VoiceConnections[0];

		const encoder = newInfo.voiceConnection.createExternalEncoder({
			type: 'ffmpeg',
			source: `./modules/sound/sounds/${song}.mp3`,
			frameDuration: 60,
			format: 'opus',
			inputArgs: [],
			outputArgs: [],
			debug: true,
		});
		if (!encoder) return console.log('Voice connection is no longer valid'); // eslint-disable-line

		encoder.once('end', () => {
			e.message.channel.sendMessage(`Finished playing ${song}`);
		});

		const encoderStream = encoder.play();
		encoderStream.resetTimestamp();
		encoderStream.removeAllListeners('timestamp');
		// encoderStream.on('timestamp', time => console.log(`${time} time`)); // eslint-disable-line
		return true;
	}


	if (s(message).contains('vplay=') || message === 'vstop' || message === 'vleave') {
		const splitMessage = message.split('=');
		const song = splitMessage[1];

		if (s(message).contains('vplay=')) {
			if (!client.VoiceConnections.length) {
				return e.message.reply('Not connected to any channel');
			}
			const info = client.VoiceConnections.getForGuild(guild);
			e.message.channel.sendMessage(`Attempting to play ${song}`);
			if (info) play(info, song);
		} else if (message === 'vleave') {
			stop();

			client.Channels
				.filter(channel => channel.type === 'voice')
				.forEach(channel => {
					if (channel.joined) {
						channel.leave();
					}
				});

			e.message.channel.sendMessage('Disconnected!');
		} else {
			stop();
			e.message.channel.sendMessage('Stopping...');
		}
	}

	return true;
}
