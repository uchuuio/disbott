var info = function (Config, bot, channelID, message) {
	if (message === 'info') {
		// version, uptime
		bot.sendMessage({
			to: channelID,
			message: 'Disbott Version ' + process.env.npm_package_version + ', ' + Config.domain,
		});
	}
};

module.exports = info;
