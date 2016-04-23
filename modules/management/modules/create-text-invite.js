import _ from 'underscore';
import S from 'string';
const s = S;

export default function createTextInvite(e, message) {
	if (s(message).contains('createtextinvite=')) {
		const splitMessage = message.split('=');
		const inviteChannel = splitMessage[1];

		const server = e.message.channel.guild;
		const channels = server.textChannels;

		_.each(channels, (channel) => {
			if (channel.name === inviteChannel) {
				const expiresIn = (60 * 60) * 6; // 6hours
				channel.createInvite({
					max_age: expiresIn,
				}).then((res) => {
					let inviteMessage = 'The following code/link lasts for 6hours\r\n';
					inviteMessage += `The instant invite code is ${res.code}"\r\n"`;
					inviteMessage += `And the instant invite link is http://discord.gg/${res.code}`;

					e.message.channel.sendMessage(inviteMessage);
				});
			}
		});
	}
};
