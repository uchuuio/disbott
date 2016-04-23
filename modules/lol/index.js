import lolSetSummoner from './modules/set-summoner';
import lolGetSetSummoner from './modules/get-set-summoner';
import lolCurrentGameInfo from './modules/current-game';
import lolRankedStats from './modules/ranked-stats';

export default function league(e, message) {
	const lcmessage = message.toLowerCase();
	lolSetSummoner(e, lcmessage);
	lolGetSetSummoner(e, lcmessage);
	lolCurrentGameInfo(e, lcmessage);
	lolRankedStats(e, lcmessage);
}
