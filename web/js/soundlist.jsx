var Sound = React.createClass({
   render: function() {
       return (
           <li>{ this.props.file } // <span className="code">!playsound={ this.props.file }</span></li>
       );
   } 
});

var SoundlistApp = React.createClass({
    getInitialState: function() {
        return {
            loading: true
        };
    },
    
    getCurrentGameData: function() {
        var that = this;

        $.ajax({
            url: '/api/sounds/',
            dataType: 'json',
            success: function(data) {
                console.log(data);
                this.setState({
                    loading: false,
                    sounds: data
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
                <div className="soundlist-area">
                    <h1 className="tc">Loading Disbott's Soundlist</h1>
                </div>
            );
        } else {          
            var SoundlistRows = this.state.sounds.map(function(data) {
                return (
                    <Sound
                        file={data}
                    />
                );
            });
            
            return (
                <div className="soundlist-area">
                    <h1>Disbott's Soundlist</h1>
                    <ul>
                        { SoundlistRows }
                    </ul>
                </div>
            );
        }
    }
});

ReactDOM.render(
    <SoundlistApp />,
    document.getElementById('app')
);