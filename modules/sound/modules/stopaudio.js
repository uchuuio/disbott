var stopAudio = function(bot, channelID, message, voiceChannelID) {
    if (message === "!stopaudio") {
        bot.sendMessage({
            to: channelID,
            message: "Stopping Audio..."
        }, function() {
            bot.getAudioContext(voiceChannelID, function(stream) {
                stream.stopAudioFile();
                stream.once('fileEnd', function() {
                    bot.sendMessage({
                        to: channelID,
                        message: "Stopped!"
                    });
                });
            });
        });
    }
}

module.exports = stopAudio;
