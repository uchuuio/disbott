var remindmeDb = require('./remindmeDb');

var _ = require('underscore');
var moment = require('moment');

var remindme = function (bot) {
    setInterval(function () {
        remindmeDb.find({ time: moment().format() }, function (err, reminders) {
            if (reminders.length > 0) {
                _.each(reminders, function (reminder) {
                    bot.sendMessage({
                        to: reminder.userID,
                        message: 'Hi, this is disbott reminding you: '+ reminder.remindMessage
                    })
                });
            }
        });
    }, 1000);
}

module.exports = remindme;