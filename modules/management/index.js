// var Datastore = require('nedb');
// var managementDb = new Datastore({
//    filename: './datastores/management.db',
//    autoload: true 
// });
var _ = require('underscore');
var S = require('string');

var createTextInvite = require('./modules/create-text-invite');
var createVoiceInvite = require('./modules/create-voice-invite');

var management = function(Config, bot, channelID, message, rawEvent) {
    createTextInvite(_, S, bot, channelID, message);
    createVoiceInvite(_, S, bot, channelID, message);
}

module.exports = management;
