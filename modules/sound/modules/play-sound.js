import lame from 'lame';
import fs from 'fs';
import S from 'string';

export default function playSound(client, e, message, stopPlaying) {
	if (S(message).contains('vplay=') || message === 'vstop') {
		var splitMessage = message.split('=');
		var song = splitMessage[1];

		if (S(message).contains('vplay=')) {
			e.message.channel.sendMessage(`Attempting to play ${song}`);
		} else {
			song = null;
			e.message.channel.sendMessage('Stopping...');
		}

		play(client, client.VoiceConnections[0], song, e, stopPlaying);
	}
};

function play(client, voiceConnectionInfo, song, e, stopPlaying = false) {
	var mp3decoder = new lame.Decoder();
	mp3decoder.on('format', decode);
	if (song === null) {
		fs.createReadStream(`./modules/sound/sounds/unicorn.mp3`).pipe(mp3decoder);
	} else {
		fs.createReadStream(`./modules/sound/sounds/${song}.mp3`).pipe(mp3decoder);
	}

	function decode(pcmfmt) {
		// note: discordie encoder does resampling if rate != 48000
		var options = {
			frameDuration: 60,
			sampleRate: pcmfmt.sampleRate,
			channels: pcmfmt.channels,
			float: false,
		};

		const frameDuration = 60;

		var readSize =
			pcmfmt.sampleRate / 1000 *
			options.frameDuration *
			pcmfmt.bitDepth / 8 *
			pcmfmt.channels;

		mp3decoder.once('readable', function () {
			if (!client.VoiceConnections.length) {
				return console.log('Voice not connected');
			}

			var voiceConnection = voiceConnectionInfo.voiceConnection;

			// 一 encoder per voice connection
			var encoder = voiceConnection.getEncoder(options);

			const needBuffer = () => encoder.onNeedBuffer();
			encoder.onNeedBuffer = function () {
				var chunk = mp3decoder.read(readSize);
				if (stopPlaying) return;

				// delay the packet if no data buffered
				if (!chunk) return setTimeout(needBuffer, options.frameDuration);

				var sampleCount = readSize / pcmfmt.channels / (pcmfmt.bitDepth / 8);
				encoder.enqueue(chunk, sampleCount);
			};

			needBuffer();
		});

		mp3decoder.once('end', function () {
			e.message.channel.sendMessage(`Finished playing ${song}`);
		});
	}
}
