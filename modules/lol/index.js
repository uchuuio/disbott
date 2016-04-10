import lolSetSummoner from './modules/set-summoner';
import lolGetSetSummoner from './modules/get-set-summoner';
import lolCurrentGameInfo from './modules/current-game';
import lolRankedStats from './modules/ranked-stats';

export default function league(e, message) {
	message = message.toLowerCase();
	lolSetSummoner(e, message);
	lolGetSetSummoner(e, message);
	lolCurrentGameInfo(e, message);
	lolRankedStats(e, message);
};
