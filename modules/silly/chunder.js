var chunder = function (bot, channelID, message) {
	if (message === 'who is the chunder king?') {
		bot.sendMessage({
			to: channelID,
			message: '@tomopagu is the chunder king!',
		});
	}
};

module.exports = chunder;
