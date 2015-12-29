var disconnect = function(bot, channelID, message, voiceChannelID) {
    if (message === "!sounddisconnect") {
        bot.sendMessage({
            to: channelID,
            message: "Disconnecting from Voice Server"
        }, function() {
            bot.leaveVoiceChannel(voiceChannelID);
        });
    }
}

module.exports = disconnect;
