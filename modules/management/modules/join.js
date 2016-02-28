var join = function (S, bot, channelID, message) {
	if (S(message).contains('join=')) {
		var splitMessage = message.split('=');
		var code = splitMessage[1];

		bot.acceptInvite(code, function (res) {
			bot.sendMessage({
				to: channelID,
				message: 'Joining channel: ' + res.channel.name + ' on ' + res.guild.name,
			});
		});
	}
};

module.exports = join;
