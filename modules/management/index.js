// var Datastore = require('nedb');
// var managementDb = new Datastore({
//	filename: './datastores/management.db',
//	autoload: true
// });
import _ from 'underscore';
import S from 'string';

import join from './modules/join';
import createTextInvite from './modules/create-text-invite';
import createVoiceInvite from './modules/create-voice-invite';

export default function management(Config, bot, channelID, message, rawEvent) {
	createTextInvite(_, S, bot, channelID, message);
	createVoiceInvite(_, S, bot, channelID, message);
	join(S, bot, channelID, message);
};
