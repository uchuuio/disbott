var gameMode = function(game) {
    if (game.gameType === 'CUSTOM_GAME') {
        return 'Custom';
    } else if (game.gameType === 'MATCHED_GAME') {
        if (game.gameQueueConfigId === 0) {
            return 'Custom';
        } else if (game.gameQueueConfigId === 2) {
            return 'Blind Pick';
        } else if (game.gameQueueConfigId === 4) {
            return 'Solo Queue';
        } else if (game.gameQueueConfigId === 8) {
            return 'Twisted Treeline';
        } else if (game.gameQueueConfigId === 14) {
            return 'Draft Pick';
        } else if (game.gameQueueConfigId === 16) {
            return 'Blind Dominion';
        } else if (game.gameQueueConfigId === 31) {
            return 'AI vs BOT Classic - Intro';
        } else if (game.gameQueueConfigId === 32) {
            return 'AI vs BOT Classic - Beginner';
        } else if (game.gameQueueConfigId === 33) {
            return 'AI vs BOT Classic - Intermediate';
        } else if (game.gameQueueConfigId === 41) {
            return 'Ranked Treeline';
        } else if (game.gameQueueConfigId === 42) {
            return 'Ranked 5s';
        } else if (game.gameQueueConfigId === 52) {
            return 'AI vs BOT Treeline';
        } else if (game.gameQueueConfigId === 61) {
            return 'Team Builder';
        } else if (game.gameQueueConfigId === 65) {
            return 'ARAM';
        } else if (game.gameQueueConfigId === 300) {
            return 'Poro King';
        }
    }
}

module.exports = gameMode;