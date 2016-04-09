export default function playSound(S, bot, channelID, message, voiceChannelID) {
	if (S(message).contains('playsound=')) {
		var splitMessage = message.split('=');
		var song = splitMessage[1];
		bot.sendMessage({
			to: channelID,
			message: 'Playing ' + song,
		}, function () {
			bot.getAudioContext({ channel: voiceChannelID, stereo: true }, function (stream) {
				stream.playAudioFile('./modules/sound/sounds/' + song);
				stream.once('fileEnd', function () {
					bot.sendMessage({
						to: channelID,
						message: 'Finished Playing ' + song,
					});
				});
			});
		});
	}
};
