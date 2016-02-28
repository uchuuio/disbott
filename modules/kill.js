var kill = function (bot, channelID, message) {
	if (message === 'killdisbot') {
		bot.sendMessage({
			to: channelID,
			message: 'Killing self, brb'
		}, function () {
			bot.disconnect();
		});
	}
};

module.exports = kill;
