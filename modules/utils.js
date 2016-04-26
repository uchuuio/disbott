import { Config } from './../config';

// About Command
export function about(e, message) {
	if (message === 'about') {
		e.message.channel.sendMessage(`Hello, I\'m Disbott. A bot for Discord. Find out more about me here ${Config.domain}`);
	}
}

// Help Command
export function help(e, message) {
	if (message === 'help') {
		e.message.channel.sendMessage(`You can find my command list here: ${Config.domain}/#commands`);
	}
}

// Info Command
export function info(e, message) {
	if (message === 'info') {
		e.message.channel.sendMessage(`Disbott Version ${process.env.npm_package_version}, ${Config.domain}`);
	}
}

// Ping Command
export function ping(e, message) {
	if (message === 'ping') {
		e.message.channel.sendMessage(`${e.message.author.mention}, pong`);
	}
}

// User has joined
export function userHasJoinedVoiceChannel(e) {
	if (e.channel.position === 0) {
		const channel = e.channel.guild.generalChannel;
		channel.sendMessage(`${e.user.username} joined ${e.channel.name}`, true).then((result) => {
			result.delete();
		});
	}
}

export function deleteMessages(e, message) {
	if (message === 'deleteall') {
		const channel = e.message.channel;
		channel.sendMessage('Attempting to delete messages');
		channel.fetchMessages(100).then(() => {
			channel.messages.forEach((messageData) => {
				messageData.delete();
			});
		});
	}
}
