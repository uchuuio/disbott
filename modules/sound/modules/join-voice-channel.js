export default function joinVoiceChannel(e, message) {
	var author = e.message.member;
	var authorVoiceChannel = author.getVoiceChannel();

	authorVoiceChannel.join().then(function (res) {
		e.message.channel.sendMessage('Ready to play!');
	});
};
