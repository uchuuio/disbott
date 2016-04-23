import Datastore from 'nedb';
export const remindmeDb = new Datastore({
	filename: './datastores/remindme.db',
	autoload: true,
});
