var w = window.innerWidth * window.devicePixelRatio,
    h = window.innerHeight * window.devicePixelRatio;

var Winterfold = Winterfold || {};

Winterfold.game = new Phaser.Game(w, h, Phaser.AUTO, 'gameContainer');

app = {
  SQUARE_SIZE: 160
};

//global object for font loading
Winterfold.game.WebFont = {
  //call rungame when fonts are loaded
  active: function() {
		runGame()
  },
  custom: {
    //array of family names, the ones written within the stylesheet.css coming
    //in the fontSquirrel's webfont kit
    families: ['sumiremedium'],
    //local path to stylesheet.css
    urls: ["css/fonts/stylesheet.css"]
  }
};

// =============================================================================
// PHASER STATES
// =============================================================================


Winterfold.game.state.add('BootState', Winterfold.BootState);
Winterfold.game.state.add('PreloaderState', Winterfold.PreloaderState);
Winterfold.game.state.add('MovieState', Winterfold.MovieState);
Winterfold.game.state.add('TitleState', Winterfold.TitleState);
Winterfold.game.state.add('TestState', Winterfold.TestState);

// =============================================================================
// LOAD NEW STATE
// =============================================================================

Winterfold.game.state.start('BootState');
