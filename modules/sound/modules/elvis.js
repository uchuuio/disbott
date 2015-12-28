var elvis = function(bot, channelID, message, stream) {
    if (message === "!elvis") {
        bot.sendMessage({
            to: channelID,
            message: "Playing THE KING"
        }, function() {
            bot.getAudioContext('130758048200392705', function(stream) {
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