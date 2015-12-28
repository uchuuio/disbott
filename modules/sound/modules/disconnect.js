var disconnect = function(bot, channelID, message, stream) {
    if (message === "!sounddisconnect") {
        bot.sendMessage({
            to: channelID,
            message: "Disconnecting from Voice Server"
        }, function() {
            bot.leaveVoiceChannel("130758048200392705");
        });
    }
}

module.exports = disconnect;
