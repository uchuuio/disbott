import Datastore from 'nedb';

export var leagueDb = new Datastore({
	filename: './datastores/league.db',
	autoload: true,
});
