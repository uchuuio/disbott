export default function stopSound(bot, channelID, message, voiceChannelID) {
	if (message === 'stopsound') {
		bot.sendMessage({
			to: channelID,
			message: 'Stopping Audio...',
		}, function () {
			bot.getAudioContext(voiceChannelID, function (stream) {
				stream.stopAudioFile();
				stream.once('fileEnd', function () {
					bot.sendMessage({
						to: channelID,
						message: 'Stopped!',
					});
				});
			});
		});
	}
};
