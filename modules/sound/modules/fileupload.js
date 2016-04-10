var fs = require('fs');
var url = require('url');
var http = require('http');
var exec = require('child_process').exec;
var spawn = require('child_process').spawn;

var DOWNLOAD_DIR = './modules/sound/sounds/';

export default function soundFileupload(e) {
	if (e.message.isPrivate) {
		if (e.message.attachments.length > 0) {
			var fileUrl = e.message.attachments[0].url;
			var fileExt = e.message.attachments[0].url.split('.').pop();
			var niceFileName = e.message.attachments[0].filename.split('.').shift();

			// Function to download file using HTTP.get
			var downloadFileHttpget = function (fileUrl) {
				var options = {
					host: url.parse(fileUrl).host,
					port: 80,
					path: url.parse(fileUrl).pathname,
				};

				var fileName = url.parse(fileUrl).pathname.split('/').pop();
				var file = fs.createWriteStream(DOWNLOAD_DIR + fileName);

				http.get(options, function (res) {
					res.on('data', function (data) {
						file.write(data);
					}).on('end', function () {
						file.end();
						e.message.channel.sendMessage(`${niceFileName} uploaded!\r\nYou can use \`vplay=${niceFileName}\` to hear it.`);
					});
				});
			};

			if (fileExt === 'mp3') {
				e.message.channel.sendMessage(`Uploading ${niceFileName}...`);
				downloadFileHttpget(fileUrl);
			} else {
				e.message.channel.sendMessage('I only accept mp3 files');
			}
		}
	}
};
