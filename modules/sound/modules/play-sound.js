const fs = require('fs');
const path = require('path');
const childProcess = require('child_process');
import S from 'string';
const s = S;

export default function playSound(client, e, message, stopPlaying) {
	if (s(message).contains('vplay=') || message === 'vstop' || message === 'vleave') {
		const splitMessage = message.split('=');
		const song = splitMessage[1];

		if (s(message).contains('vplay=')) {
			e.message.channel.sendMessage(`Attempting to play ${song}`);
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

		play();
	}

	function getConverter(args, options) {
		const binaries = [
			'ffmpeg',
			'ffmpeg.exe',
			'avconv',
			'avconv.exe',
		];

		const paths = process.env.PATH.split(path.delimiter).concat(['.']);

		for (const name of binaries) {
			for (const p of paths) {
				const binary = p + path.sep + name;
				if (!fs.existsSync(binary)) continue;
				return childProcess.spawn(name, args, options);
			}
		}

		return null;
	}

	let ffmpeg = null;
	function stop() {
		stopPlaying = true;
		if (!ffmpeg) return;
		ffmpeg.kill();
		ffmpeg = null;
	}

	function play(voiceConnectionInfo) {
		stopPlaying = false;

		const sampleRate = 48000;
		const bitDepth = 16;
		const channels = 1;

		if (ffmpeg) ffmpeg.kill();

		ffmpeg = getConverter([
			'-re',
			'-i', 'elvis.mp3',
			// '-i', song + '.mp3',
			'-f', 's16le',
			'-ar', sampleRate,
			'-ac', channels,
			'pipe:1',
		], { stdio: ['pipe', 'pipe', 'ignore'] });
		if (!ffmpeg) return console.log('ffmpeg/avconv not found'); // eslint-disable-line

		const ffmpegB = ffmpeg;
		const ff = ffmpeg.stdout;

		// note: discordie encoder does resampling if rate != 48000
		const options = {
			frameDuration: 60,
			sampleRate,
			channels,
			float: false,
		};

		const frameDuration = 60;

		const readSize =
		sampleRate / 1000 *
		options.frameDuration *
		bitDepth / 8 *
		channels;

		ff.once('readable', () => {
			if (!client.VoiceConnections.length) {
				return console.log('Voice not connected'); // eslint-disable-line
			}

			if (!voiceConnectionInfo) {
				// get first if not specified
				voiceConnectionInfo = client.VoiceConnections[0];
			}

			const voiceConnection = voiceConnectionInfo.voiceConnection;

			// 一 encoder per voice connection
			const encoder = voiceConnection.getEncoder(options);

			const needBuffer = () => encoder.onNeedBuffer();
			encoder.onNeedBuffer = () => {
				const chunk = ff.read(readSize);
				console.log(ff); // eslint-disable-line

				if (ffmpegB.killed) return;
				if (stopPlaying) return stop();

				// delay the packet if no data buffered
				if (!chunk) return setTimeout(needBuffer, options.frameDuration);

				const sampleCount = readSize / channels / (bitDepth / 8);
				encoder.enqueue(chunk, sampleCount);
			};

			needBuffer();
		});

		ff.once('end', () => {
			if (stopPlaying) return;
			// setTimeout(play, 100, voiceConnectionInfo);
			e.message.channel.sendMessage(`Finished playing ${song}`);
		});
	}
}
