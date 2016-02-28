var Datastore = require('nedb');
var db = new Datastore({
	filename: './datastores/league.db',
	autoload: true 
});

module.exports = db;
