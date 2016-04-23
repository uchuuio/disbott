FROM dkarchmervue/fluent-ffmpeg

ADD . /app

WORKDIR /app

RUN npm install

ENTRYPOINT exec ./node_modules/.bin/babel-node --presets es2015 -- ./server.js
