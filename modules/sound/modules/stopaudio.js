var stopAudio = function(bot, channelID, message, stream) {
    if (message === "!stopaudio") {
        bot.sendMessage({
            to: channelID,
            message: "Stopping Audio..."
        }, function() {
            bot.getAudioContext('130758048200392705', function(stream) {
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
