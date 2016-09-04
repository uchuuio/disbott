import test from 'ava';

import { Config } from './config';

import Discordie from 'discordie';
const client = new Discordie();

function sendMessage(message, toBot) {
	const guild = client.Guilds.find(g => g.name === 'Disbott Test');
	const channel = guild.textChannels.find(c => c.name === 'tests');
	const botUser = guild.members.find(m => m.username === 'disbott-dev');
	if (toBot) {
		return channel.sendMessage(`${botUser.mention} ${message}`);
	}
	return channel.sendMessage(message);
}

function returnMessage() {
	client.Dispatcher.on('MESSAGE_CREATE', e => {
		return e.message.content;
	});
}

test.serial.cb.before('Can connect to Discord', t => {
	client.connect({
		token: Config.discord.testerToken,
	});

	client.Dispatcher.on('GATEWAY_READY', () => {
		sendMessage('Hello! I\'m the Disbott Tester. Beginning Tests').then((result, error) => {
			if (error) {
				console.log(error);
				t.fail();
			}

			t.pass();
			t.end();
		});
	});
});

test.serial.cb('Check ping command', t => {
	sendMessage('ping', true).then((result, error) => {
		if (error) { t.fail(); }

		setTimeout(() => {
			const messages = client.Messages.toArray();
			const message = messages.slice(-1)[0];
			t.is(message.content, `<@${client.User.id}>, pong`);
			t.end();
		}, 300);
	});
});

test.serial.cb('Check about command', t => {
	sendMessage('about', true).then((result, error) => {
		if (error) { t.fail(); }

		setTimeout(() => {
			const messages = client.Messages.toArray();
			const message = messages.slice(-1)[0];
			t.is(message.content, 'Hello, I\'m Disbott. A bot for Discord. Find out more about me here https://disbott.pagu.co');
			t.end();
		}, 300);
	});
});

test.serial.cb('Check help command', t => {
	sendMessage('help', true).then((result, error) => {
		if (error) { t.fail(); }

		setTimeout(() => {
			const messages = client.Messages.toArray();
			const message = messages.slice(-1)[0];
			t.is(message.content, 'You can find my command list here: https://disbott.pagu.co/#commands');
			t.end();
		}, 300);
	});
});

test.serial.cb('Check info command', t => {
	sendMessage('info', true).then((result, error) => {
		if (error) { t.fail(); }

		setTimeout(() => {
			const messages = client.Messages.toArray();
			const message = messages.slice(-1)[0];
			t.is(message.content, 'Disbott Version 1.0.0, https://disbott.pagu.co');
			t.end();
		}, 300);
	});
});

test.after('Disconnect', (t) => {
	return sendMessage('deleteall', true).then((result) => {
		client.disconnect();
		t.pass();
	});
});
