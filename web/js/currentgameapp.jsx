var CurrentGameData = React.createClass({
   render: function() {
       return (
           <tr>
                <td></td>
                <td>{ this.props.blueSide.summonerName }</td>
                <td>{ this.props.blueSide.champion }</td>
            
                <td>&nbsp;</td>
            
                <td></td>
                <td>{ this.props.redSide.summonerName }</td>
                <td>{ this.props.redSide.champion }</td>
            </tr>
       );
   } 
});

var CurrentGameApp = React.createClass({
    getInitialState: function() {
        var params = getQueryParams(document.location.search);
        return {
            summonerID: params.summonerID,
            loading: true
        };
    },
    
    getCurrentGameData: function() {
        var that = this;

        $.ajax({
            url: '/api/lol/current-game?summonerID='+this.state.summonerID,
            dataType: 'json',
            success: function(data) {
                console.log(data);
                this.setState({
                    loading: false,
                    currentGameData: data
                });
            }.bind(this),
            error: function(xhr, status, err) {
                console.error('error', status, err.toString());
            }.bind(this)
        });
    },
    
    render: function() {
        if (this.state.loading) {
            this.getCurrentGameData();
            return (
                <div className="current-game-area">
                    <h1 className="tc">Loading A Current Game</h1>
                </div>
            );
        } else {
            var CurrentGameDataRowNumbers = (this.state.currentGameData.participants.length / 2);
            var CurrentGameDataRowData = [];
            for (var i = 0; i < CurrentGameDataRowNumbers; i++) {
                var redSideNumber = i + 5;
                CurrentGameDataRowData.push({
                    blueSide: this.state.currentGameData.participants[i],
                    redSide: this.state.currentGameData.participants[redSideNumber]
                });
            }
            
            var CurrentGameDataRows = CurrentGameDataRowData.map(function(data) {
                return (
                    <CurrentGameData
                        blueSide={data.blueSide}
                        redSide={data.redSide}
                    />
                );
            });
            
            return (
                <div className="current-game-area">
                    <h1>{ this.state.currentGameData.gameMode }: { this.state.currentGameData.gameType }</h1>
                    <table className="w-100">
                        <thead>
                            <tr>
                                <td className="tc" colSpan="3">Blue</td>
                                <td className="tc" rowSpan="2">v</td>
                                <td className="tc" colSpan="3">Red</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>Summoner Name</td>
                                <td>Champion</td>
                            
                                <td></td>
                                <td>Summoner Name</td>
                                <td>Champion</td>
                            </tr>
                        </thead>
                        <tbody>
                            { CurrentGameDataRows }
                        </tbody>
                        <tfoot>
                            <tr>
                                <td colSpan="7">Champions may not appear due to hitting the rate limits of Riots API, once disbot is 1.0.0 I'll go to Riot and get a production key. Also once we have that I can add more information to this such as runes/masteries/rank</td>
                            </tr>
                        </tfoot>
                    </table>
                </div>
            );
        }
    }
});

function getQueryParams(qs) {
    qs = qs.split("+").join(" ");
    var params = {},
        tokens,
        re = /[?&]?([^=]+)=([^&]*)/g;

    while (tokens = re.exec(qs)) {
        params[decodeURIComponent(tokens[1])]
            = decodeURIComponent(tokens[2]);
    }

    return params;
}


ReactDOM.render(
    <CurrentGameApp />,
    document.getElementById('app')
);