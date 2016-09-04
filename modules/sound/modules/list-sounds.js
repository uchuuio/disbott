import { Config } from './../../../config.js';

export default function listSounds(e, message) {
	if (message === 'listsounds') {
		e.message.channel.sendMessage(`View our list of sounds here: ${Config.domain}/soundlist.html`);
	}
}
