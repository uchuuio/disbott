var Datastore = require('nedb');
var soundDb = new Datastore({
   filename: './datastores/sound.db',
   autoload: true 
});

var disconnect = require('./modules/disconnect');
var stopAudio = require('./modules/stopaudio');
var elvis = require('./modules/elvis');

var sound = function(bot, channelID, message, soundInitialised) {
    if (message === "!sound") {
        bot.sendMessage({
            to: channelID,
            message: "Getting ready to play..."
        }, function() {
            bot.joinVoiceChannel('130758048200392705', function() {
                var data = {
                    enabled: true
                };
                soundDb.insert(data, function (err, newDoc) {
                    // 
                });
            })
        });
    } else {
        // Modules
        soundDb.find({ enabled: true }, function (err, data) {
            var stream = data[0].stream;
            elvis(bot, channelID, message, stream);
            stopAudio(bot, channelID, message, stream);
            disconnect(bot, channelID, message);
        });
    }
    
    bot.on('disconnect', function() {
        soundDb.remove({ enabled: true }, { multi: true }, function (err, numRemoved) {
           console.log(numRemoved); 
        });
        soundDb.remove({ enabled: false }, { multi: true }, function (err, numRemoved) {
           console.log(numRemoved); 
        });
    });
}

module.exports = sound;
