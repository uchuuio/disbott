import { Config } from './../../config';

import Twit from 'twit';
var T = new Twit({
	consumer_key: Config.twitter.consumer_key,
	consumer_secret: Config.twitter.consumer_secret,
	access_token: Config.twitter.access_token,
	access_token_secret: Config.twitter.access_token_secret,
});

import { mirin } from './modules/mirin';
import { gazo } from './modules/gazo';
import { headline } from './modules/headline';

export default function twitter(e, message) {
	mirin(T, e, message);
	headline(T, e, message);
	gazo(T, e, message);
};
