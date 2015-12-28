var getSummonerIdFunction = function(_, lolapi, leagueDb, userID, getSummonerId, summonerName) {
    if (_.isString(summonerName)) {
        lolapi.Summoner.getByName(summonerName, function (error, summoner) {
            if (error) {
                getSummonerId.emit('fail', 'ERROR: Couldn\'t find a summoner with that name on EUW');
            } else {
                var summonerId = summoner[summonerName].id;
                getSummonerId.emit('completed', summonerId);
            }
        });
    } else {
        if (leagueDb.find({
            discordUserId: userID
        }, function(err, summoner) {
            if (_.isEmpty(summoner)) {
                getSummonerId.emit('fail', 'ERROR: Have you linked your league account with !lolsetsummoner=[yoursummonername]?');
            } else {
                var summonerID = summoner[0].leagueSummonerId;
                getSummonerId.emit('completed', summonerID);
            }
        }));
    };
};

module.exports = getSummonerIdFunction;