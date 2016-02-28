var remindmeDb = require('./remindmeDb');

var _ = require('underscore');
var moment = require('moment');

var remindme = function (bot) {
	setInterval(function () {
		remindmeDb.find({ remindTime: moment().format() }, function (err, reminders) {
			if (reminders.length > 0) {
				_.each(reminders, function (reminder) {
					var setTime = moment(reminder.setTime).fromNow();
					bot.sendMessage({
						to: reminder.userID,
						message: 'Hi, this is disbott reminding you: ' +  reminder.remindMessage + '; from ' + setTime,
					});
				});
			}
		});
	}, 1000);
};

module.exports = remindme;
