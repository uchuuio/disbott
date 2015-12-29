var listSounds = function(bot, channelID, message, voiceChannelID) {
    if (message === "!listsounds") {
        bot.sendMessage({
            to: channelID,
            message: "View our list of sounds here: http://disbot.pagu.co/soundlist.html"
        });
    }
}

module.exports = listSounds;
