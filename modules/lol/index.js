import lolSetSummoner from './modules/set-summoner';
import lolGetSetSummoner from './modules/get-set-summoner';
import lolCurrentGameInfo from './modules/current-game';
import lolRankedStats from './modules/ranked-stats';

export default function league(bot, user, userID, channelID, message) {
	message = message.toLowerCase();
	lolSetSummoner(bot, user, userID, channelID, message);
	lolGetSetSummoner(bot, user, userID, channelID, message);
	lolCurrentGameInfo(bot, user, userID, channelID, message);
	lolRankedStats(bot, user, userID, channelID, message);
};
