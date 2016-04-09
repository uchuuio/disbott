import Datastore from 'nedb';
export var remindmeDb = new Datastore({
	filename: './datastores/remindme.db',
	autoload: true,
});
