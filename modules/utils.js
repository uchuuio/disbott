import { Config } from './../config';

// About Command
export function about(e, message) {
	if (message === 'about') {
		e.message.channel.sendMessage(`Hello, I\'m Disbott. A bot for Discord. Find out more about me here ${Config.domain}`);
	}
};

// Help Command
export function help(e, message) {
	if (message === 'help') {
		e.message.channel.sendMessage(`You can find my command list here: ${Config.domain}/#commands`);
	}
};

// Info Command
export function info(e, message) {
	if (message === 'info') {
		e.message.channel.sendMessage(`Disbott Version ${process.env.npm_package_version}, ${Config.domain}`);
	}
};

// Kill Command
export function kill(e, message) {
	if (message === 'killdisbott') {
		e.message.channel.sendMessage('Killing self, brb');
	}
};

// Ping Command
export function ping(e, message) {
	if (message === 'ping') {
		e.message.channel.sendMessage(e.message.author.mention + ', pong');
	}
};
