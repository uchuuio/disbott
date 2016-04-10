import _ from 'underscore';
import S from 'string';

export default function createVoiceInvite(e, message) {
	if (S(message).contains('createvoiceinvite=')) {
		var splitMessage = message.split('=');
		var inviteChannel = splitMessage[1];

		var server = e.message.channel.guild;
		var channels = server.voiceChannels;

		_.each(channels, function (channel) {
			if (channel.name === inviteChannel) {
				var expiresIn = (60 * 60) * 6; // 6hours
				channel.createInvite({
					max_age: 60 * 60 * 24,
				}).then(function(res) {
					var message = 'The following code/link lasts for 6hours\r\n';
					message += 'The instant invite code is ' + res.code + '.\r\n';
					message += 'And the instant invite link is http://discord.gg/' + res.code;

					e.message.channel.sendMessage(message);
				});
			}
		});
	}
};
