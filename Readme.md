# Disbott

## A bot for Discord

### Command List

You can find the latest command list here: http://disbott.pagu.co/#commands

### Install

To start with you'll need nodejs!

Next you'll want to clone this and then add a `.env` file to the root of the cloned folder with the following:

```
DISCORD_TOKEN=
LEAGUE_APIKEY=
LEAGUE_LOCATION=
TWITTER_CONSUMER_KEY=
TWITTER_CONSUMER_SECRET=
TWITTER_ACCESS_TOKEN=
TWITTER_ACCESS_TOKEN_SECRET=
```

You'll want to fill these in with your own values.

Once that's all sorted you can then run `npm install` to install the dependencies for the bot and webserver.

When that's all sorted you should be able to run `npm run start:bot` to start the bot or `npm run start:web` to start the websever on http://localhost:5000.

### Bot Modules

Each command the bot uses is it's own file in `/modules` if you're adding new modules you'll want to create new files (and folders if you're grouping a group of commands) for them. To then add or remove a command from the bot you'll want to modify `bot.js` either adding/removing the require statement & the function within `bot.on(message)`.

### Contributing

I try to keep to [Airbnb's Javascript Standards](https://github.com/airbnb/javascript) where possible using the [.eslintrc](https://github.com/tomopagu/disbott/blob/discordie/.eslintrc) file in the root of disbott. I'm happy to have any help or suggestions where possible, just stick a PR on the Repo!

#### Why the disbott rather than disbot

Funny story, I broke the first disbot user. For some reason it remained connected to discord but remains unresponsive to any message whatsoever. I couldn't send anything as that user when logged into the discord client myself. This was way before the bots we have today so disbot probably could work but the disbott name has kinda stuck.
