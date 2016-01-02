var about = function(Config, bot, channelID, message) {
    if (message === "about") {
        bot.sendMessage({
            to: channelID,
            message: "Hello, I'm Disbott. A bot for Discord. Find out more about me here " + Config.domain
        });
    }
}

module.exports = about;
