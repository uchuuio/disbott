export default function joinVoiceChannel(Config, soundDb, bot, channelID, message) {
	bot.sendMessage({
		to: channelID,
		message: 'Getting ready to play...',
	}, function () {
		var splitMessage = message.split('=');
		var inviteCode = splitMessage[1];
		bot.queryInvite(inviteCode, function (err, response) {
			if (err) return console.log(err);
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
