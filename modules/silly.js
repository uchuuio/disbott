// Chunder
// TODO: Implement this
export function setChunder(e, message) {
	if (message === 'setchunder=') {
		e.message.channel.sendMessage('<@105753535659921408> is the new chunder king!');
	}
}

export function chunder(e, message) {
	if (message === 'who is the chunder king?') {
		e.message.channel.sendMessage('<@105753535659921408> is the chunder king!');
	}
}
