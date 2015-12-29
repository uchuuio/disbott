var fs = require('fs');
var url = require('url');
var http = require('http');
var exec = require('child_process').exec;
var spawn = require('child_process').spawn;

var DOWNLOAD_DIR = './modules/sound/sounds/';

var fileupload = function(bot, channelID, rawEvent) {
    if (rawEvent.d.attachments.length > 0) {
        var file_url = rawEvent.d.attachments[0].url;
        var file_ext = rawEvent.d.attachments[0].url.split('.').pop();

        // Function to download file using HTTP.get
        var download_file_httpget = function(file_url) {
            var options = {
                host: url.parse(file_url).host,
                port: 80,
                path: url.parse(file_url).pathname
            };

            var file_name = url.parse(file_url).pathname.split('/').pop();
            var file = fs.createWriteStream(DOWNLOAD_DIR + file_name);

            http.get(options, function(res) {
                res.on('data', function(data) {
                    file.write(data);
                }).on('end', function() {
                    file.end();
                    bot.sendMessage({
                        to: channelID,
                        message: file_name + " Uploaded"
                    });
                });
            });
        };
        
        if (file_ext === 'mp3') {
            bot.sendMessage({
                to: channelID,
                message: "Uploading file..."
            }, function() {
                download_file_httpget(file_url);
            });
        }
    }
}

module.exports = fileupload;
