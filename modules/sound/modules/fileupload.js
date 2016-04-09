var fs = require('fs');
var url = require('url');
var http = require('http');
var exec = require('child_process').exec;
var spawn = require('child_process').spawn;

var DOWNLOAD_DIR = './modules/sound/sounds/';

export default function fileupload(bot, channelID, rawEvent) {
	if (rawEvent.d.attachments.length > 0) {
		var fileUrl = rawEvent.d.attachments[0].url;
		var fileExt = rawEvent.d.attachments[0].url.split('.').pop();

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
					bot.sendMessage({
						to: channelID,
						message: fileName + ' Uploaded'
					});
				});
			});
		};

		if (fileExt === 'mp3') {
			bot.sendMessage({
				to: channelID,
				message: 'Uploading file...'
			}, function () {
				downloadFileHttpget(fileUrl);
			});
		}
	}
};
