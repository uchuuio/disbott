var elvis = function(bot, channelID, message, voiceChannelID) {
    if (message === "!elvis") {
        bot.sendMessage({
            to: channelID,
            message: "Playing THE KING"
        }, function() {
            bot.getAudioContext({ channel: voiceChannelID, stereo: true}, function(stream) {
                stream.playAudioFile('./modules/sound/sounds/elvis.mp3');
                stream.once('fileEnd', function() {
                    bot.sendMessage({
                        to: channelID,
                        message: "May god rest his soul"
                    });
                });
            });
        });
    }
}

module.exports = elvis;