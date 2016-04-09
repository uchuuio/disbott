import Datastore from 'nedb';
export var lastseenDb = new Datastore({
	filename: './datastores/lastseen.db',
	autoload: true,
});
