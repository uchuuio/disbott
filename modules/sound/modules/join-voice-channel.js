var joinVoiceChannel = function (Config, soundDb, bot, channelID, message) {
	bot.sendMessage({
		to: channelID,
		message: 'Getting ready to play...',
	}, function () {
		var splitMessage = message.split('=');
		var inviteCode = splitMessage[1];
		bot.acceptInvite(inviteCode, function (response) {
			var voiceChannelID = response.channel.id;
			bot.joinVoiceChannel(voiceChannelID, function () {
				var data = {
					enabled: true,
					voiceChannelID: voiceChannelID,
				};
				soundDb.insert(data, function (err, newDoc) {});
			});
		});
	});
};

module.exports = joinVoiceChannel;
