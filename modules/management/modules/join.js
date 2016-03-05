var join = function (S, bot, channelID, message) {
	if (S(message).contains('join=')) {
		var splitMessage = message.split('=');
		var code = splitMessage[1];

		bot.acceptInvite(code, function (err, res) {
			console.log(err, res);
			if (err) {
				bot.sendMessage({
					to: channelID,
					message: err + '. Did you mean `sound=' + code + '`?',
				});
			} else {
				bot.sendMessage({
					to: channelID,
					message: 'Joining channel: ' + res.channel.name + ' on ' + res.guild.name,
				});
			}
		});
	}
};

module.exports = join;
