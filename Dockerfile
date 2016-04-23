FROM dkarchmervue/fluent-ffmpeg

ADD . /code

WORKDIR /code

RUN npm install

ENTRYPOINT exec ./node_modules/.bin/babel-node --presets es2015 -- ./server.js
