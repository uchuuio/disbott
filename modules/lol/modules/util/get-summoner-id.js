export default function getSummonerIdFunction(s, lolapi, leagueDb, userID, getSummonerId, summonerName) {
	if (!s(summonerName).isEmpty()) {
		if (s(summonerName).startsWith('<@')) {
			let discordUserId = s(summonerName).chompLeft('<@').s;
			discordUserId = s(discordUserId).chompRight('>').s;
			leagueDb.find({
				discordUserId,
			}, (err, summoner) => {
				if (s(summoner).isEmpty()) {
					getSummonerId.emit('fail', 'ERROR: Have they linked their lol account to discord with !lolsetsummoner=[yoursummonername]?');
				} else {
					const summonerID = summoner[0].leagueSummonerId;
					getSummonerId.emit('completed', summonerID);
				}
			});
		} else {
			lolapi.Summoner.getByName(summonerName, (error, summoner) => {
				if (error) {
					getSummonerId.emit('fail', 'ERROR: Couldn\'t find a summoner with that name on EUW');
				} else {
					let lolSummonerName = s(summonerName).strip(' ').s;
					lolSummonerName = lolSummonerName.toLowerCase();

					const summonerId = summoner[summonerName].id;
					getSummonerId.emit('completed', summonerId);
				}
			});
		}
	} else {
		leagueDb.find({
			discordUserId: userID,
		}, (err, summoner) => {
			if (s(summoner).isEmpty()) {
				getSummonerId.emit('fail', 'ERROR: Have you linked your league account with !lolsetsummoner=[yoursummonername]?');
			} else {
				const summonerID = summoner[0].leagueSummonerId;
				getSummonerId.emit('completed', summonerID);
			}
		});
	}
}
