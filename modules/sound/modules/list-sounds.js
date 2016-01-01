var listSounds = function(Config, bot, channelID, message, voiceChannelID) {
    if (message === "listsounds") {
        bot.sendMessage({
            to: channelID,
            message: "View our list of sounds here: " + Config.domain + "/soundlist.html"
        });
    }
}

module.exports = listSounds;
