// var Datastore = require('nedb');
// var managementDb = new Datastore({
//	filename: './datastores/management.db',
//	autoload: true
// });

import createTextInvite from './modules/create-text-invite';
import createVoiceInvite from './modules/create-voice-invite';

export default function management(e, message) {
	createTextInvite(e, message);
	createVoiceInvite(e, message);
}
