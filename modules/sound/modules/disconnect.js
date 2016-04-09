export default function disconnect(soundDb, bot, channelID, message, voiceChannelID) {
	if (message === 'sounddisconnect') {
		bot.sendMessage({
			to: channelID,
			message: 'Disconnecting from Voice Channel',
		}, function () {
			bot.leaveVoiceChannel(voiceChannelID);
			soundDb.remove({ enabled: true }, { multi: true }, function (err, numRemoved) {});

			soundDb.remove({ enabled: false }, { multi: true }, function (err, numRemoved) {});
		});
	}
};
