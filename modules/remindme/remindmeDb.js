var Datastore = require('nedb');
var remindmeDb = new Datastore({
	filename: './datastores/remindme.db',
	autoload: true,
});

module.exports = remindmeDb;
