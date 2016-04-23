const fs = require('fs');
const url = require('url');
const http = require('http');
// const exec = require('child_process').exec;
// const spawn = require('child_process').spawn;

const DOWNLOAD_DIR = './modules/sound/sounds/';

export default function soundFileupload(e) {
	if (e.message.isPrivate) {
		if (e.message.attachments.length > 0) {
			const niceFileName = e.message.attachments[0].filename.split('.').shift();

			// Function to download file using HTTP.get
			const downloadFileHttpget = (fileUrl) => {
				const options = {
					host: url.parse(fileUrl).host,
					port: 80,
					path: url.parse(fileUrl).pathname,
				};

				const fileName = url.parse(fileUrl).pathname.split('/').pop();
				const file = fs.createWriteStream(DOWNLOAD_DIR + fileName);

				http.get(options, (res) => {
					res.on('data', (data) => {
						file.write(data);
					}).on('end', () => {
						file.end();
						e.message.channel.sendMessage(`${niceFileName} uploaded!\r\nYou can use \`vplay=${niceFileName}\` to hear it.`);
					});
				});
			};

			const fileUrl = e.message.attachments[0].url;
			const fileExt = e.message.attachments[0].url.split('.').pop();

			if (fileExt === 'mp3') {
				e.message.channel.sendMessage(`Uploading ${niceFileName}...`);
				downloadFileHttpget(fileUrl);
			} else {
				e.message.channel.sendMessage('I only accept mp3 files');
			}
		}
	}
}
