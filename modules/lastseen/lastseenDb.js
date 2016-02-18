var Datastore = require('nedb');
var lastseenDb = new Datastore({
	filename: './datastores/lastseen.db',
	autoload: true
});

module.exports = lastseenDb;
