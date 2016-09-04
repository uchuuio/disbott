export default function joinVoiceChannel(e) {
	const author = e.message.member;
	const authorVoiceChannel = author.getVoiceChannel();

	authorVoiceChannel.join().then(() => {
		e.message.channel.sendMessage('Ready to play!');
	});
}
