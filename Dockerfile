FROM dkarchmervue/fluent-ffmpeg

# Quick NPM Installs
ADD ./package.json /app/package.json
RUN cd /app && npm install

# Add our App
ADD . /app

WORKDIR /app

CMD exec ./node_modules/.bin/babel-node --presets es2015 -- ./bot.js
