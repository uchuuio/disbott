const Datastore = require('nedb');
const pollDb = new Datastore({
	filename: './datastores/poll.db',
	autoload: true,
});

import startPoll from './modules/start-poll';
import votePoll from './modules/vote-poll';

export default function poll(e, message) {
	startPoll(pollDb, e, message);
	votePoll(pollDb, e, message);
}
