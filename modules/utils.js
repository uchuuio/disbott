// About Command
export function about(Config, bot, channelID, message) {
	if (message === 'about') {
		bot.sendMessage({
			to: channelID,
			message: 'Hello, I\'m Disbott. A bot for Discord. Find out more about me here ' + Config.domain,
		});
	}
};

// Help Command
export function help(Config, bot, channelID, message) {
	if (message === 'help') {
		bot.sendMessage({
			to: channelID,
			message: 'You can find my command list here: ' + Config.domain + '/#commands',
		});
	}
};

// Info Command
export function info(Config, bot, channelID, message) {
	if (message === 'info') {
		// version, uptime
		bot.sendMessage({
			to: channelID,
			message: 'Disbott Version ' + process.env.npm_package_version + ', ' + Config.domain,
		});
	}
};

// Kill Command
export function kill(bot, channelID, message) {
	if (message === 'killdisbott') {
		bot.sendMessage({
			to: channelID,
			message: 'Killing self, brb',
		}, function () {
			bot.disconnect();
		});
	}
};

// Ping Command
export function ping(bot, channelID, message) {
	if (message === 'ping') {
		bot.sendMessage({
			to: channelID,
			message: 'pong',
		});
	}
};
