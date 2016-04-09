export default function listSounds(Config, bot, channelID, message, voiceChannelID) {
	if (message === 'listsounds') {
		bot.sendMessage({
			to: channelID,
			message: 'View our list of sounds here: ' + Config.domain + '/soundlist.html'
		});
	}
};
