var help = function(bot, channelID, message) {
    if (message === "!help") {
        bot.sendMessage({
            to: channelID,
            message: "You can find my command list here: http://disbot.pagu.co/commands"
        });
    }
}

module.exports = help;
