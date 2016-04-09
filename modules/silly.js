// Chunder
export function chunder(bot, channelID, message) {
	if (message === 'who is the chunder king?') {
		bot.sendMessage({
			to: channelID,
			message: '<@105753535659921408> is the chunder king!',
		});
	}
};
