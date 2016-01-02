var createVoiceInvite = function (_, S, bot, channelID, message) {
    if (S(message).contains("createvoiceinvite=")) {
        var splitMessage = message.split('=');
        var inviteChannel = splitMessage[1];

        _.each(bot.servers, function (server) {
            _.each(server.channels, function (channel) {
                if (channel.type === 'voice' && channel.name === inviteChannel) {
                    var expiresIn = (60*60)*6; // 6hours
                    bot.createInvite({
                        channel: channel.id,
                        max_age: expiresIn,
                        temporary: true
                    }, function(res) {
                        bot.sendMessages(channelID, [
                            "The following code/link lasts for 6hours",
                            "The instant invite code is " + res.code + ".",
                            "And the instant invite link is http://discord.gg/" + res.code
                        ])
                    });
                }
            });
        });

    }
};

module.exports = createVoiceInvite;