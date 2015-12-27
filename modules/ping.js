var ping = function(bot, channelID, message) {
    if (message === "!ping") {
        bot.sendMessage({
            to: channelID,
            message: "pong"
        });
    }
}

module.exports = ping;
