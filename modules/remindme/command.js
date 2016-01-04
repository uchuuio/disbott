var remindmeDb = require('./remindmeDb');

var S = require('string');
var moment = require('moment');

var remindmeCommand = function (bot, user, userID, channelID, message) {
    if (S(message).contains("remindme=")) {
        var splitMessage = message.split('=');
        var timeAndMessage = splitMessage[1].split(';');
        
        var timeTo = timeAndMessage[0].split('.');
        var countTimeToArray = timeTo.length / 2;
        var time = moment();
        for (var i = 0; i < countTimeToArray; i++) {
            var amountOfTime = timeTo[i];
            var unitOfTime = timeTo[i+1];
            time = time.add(amountOfTime, unitOfTime);
            i++;
        }
        time = time.format();
        var timeToNice = timeTo.join(' ');

        var remindMessage = timeAndMessage[1];
        
        var data = {
            userID: userID,
            time: time,
            remindMessage: remindMessage
        };
        
        remindmeDb.insert(data, function (err, newDoc) {
            bot.sendMessage({
                to: channelID,
                message: "I will remind you (<@" + userID + ">) in " + timeToNice + ": " + remindMessage
            });
        });
    }
}

module.exports = remindmeCommand;