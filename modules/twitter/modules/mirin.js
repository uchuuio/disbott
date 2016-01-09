var moment = require('moment');

var mirin = function (T, bot, channelID, message) {
    if (message === "mirin") {
        T.get("statuses/user_timeline", {
            screen_name: "FurukawaMirin",
            count: 1
        }, function (err, data) {
            var tweet = data[0];
            bot.sendMessages(channelID, [
                "Mirin last said: \"" + tweet.text + "\" approx. " + moment(tweet.created_at).fromNow(),
                "https://twitter.com/FurukawaMirin/status/" + tweet.id
            ]);
        });
    }
}

module.exports = mirin;