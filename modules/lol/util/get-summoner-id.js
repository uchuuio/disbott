var getSummonerIdFunction = function(S, lolapi, leagueDb, userID, getSummonerId, summonerName) {
    if (!S(summonerName).isEmpty()) {
        if (S(summonerName).startsWith('<@')) {
            userID = S(summonerName).chompLeft('<@').s;
            userID = S(userID).chompRight('>').s;
            if (leagueDb.find({
                discordUserId: userID
            }, function(err, summoner) {
                if (S(summoner).isEmpty()) {
                    getSummonerId.emit('fail', 'ERROR: Have they linked their lol account to discord with !lolsetsummoner=[yoursummonername]?');
                } else {
                    var summonerID = summoner[0].leagueSummonerId;
                    getSummonerId.emit('completed', summonerID);
                }
            }));
        } else {
            lolapi.Summoner.getByName(summonerName, function (error, summoner) {
                if (error) {
                    getSummonerId.emit('fail', 'ERROR: Couldn\'t find a summoner with that name on EUW');
                } else {
                    summonerName = S(summonerName).strip(' ').s;
                    var summonerId = summoner[summonerName].id;
                    getSummonerId.emit('completed', summonerId);
                }
            });
        }
    } else {
        if (leagueDb.find({
            discordUserId: userID
        }, function(err, summoner) {
            if (S(summoner).isEmpty()) {
                getSummonerId.emit('fail', 'ERROR: Have you linked your league account with !lolsetsummoner=[yoursummonername]?');
            } else {
                var summonerID = summoner[0].leagueSummonerId;
                getSummonerId.emit('completed', summonerID);
            }
        }));
    };
};

module.exports = getSummonerIdFunction;