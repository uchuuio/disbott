import Datastore from 'nedb';

export const leagueDb = new Datastore({
	filename: './datastores/league.db',
	autoload: true,
});
