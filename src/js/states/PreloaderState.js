var Winterfold = Winterfold || {};

Winterfold.PreloaderState = function() {
    "use strict";
    Phaser.State.call(this);
}

// Copy all of the methods from Phaser State to PreloaderState. 
Winterfold.PreloaderState.prototype = Object.create(Phaser.State.prototype);

// Retain the constructor of the prototype.
Winterfold.PreloaderState.prototype.constructor = Winterfold.PreloaderState;

Winterfold.PreloaderState.prototype.preload =  function() {
    /*
          Load all game assets
          Place your load bar, some messages.
          In this case of loading, only text is placed...
          */

    // Preload Bar
    this.preloadBar = this.add.sprite(this.world.centerX, this.world.centerY, 'preloadBar');
    this.preloadBar.anchor.setTo(0.5);
    this.preloadBar.animations.add('loading', [0, 1, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 0]);
    this.preloadBar.animations.play('loading', 20, false);
    this.preloadBar.height = (window.innerHeight * window.devicePixelRatio) * 0.3;
    this.preloadBar.width = (window.innerWidth * window.devicePixelRatio) * 0.3;

    //Load your images, spritesheets, bitmaps...
    this.load.image('winterfold-logo', '/assets/img/winterfold-logo.png')
    this.load.image('bgtest', '/assets/img/bgtest.png');
    this.load.image('grid', '/assets/img/grid.png');
    this.load.spritesheet('items', '/assets/img/items.png', 30, 30);
    // this.load.atlasJSONHash('testbg', 'assets/img/newbgpng/newbg-0.png', 'assets/img/newbgjson/newbg-0.json');
    this.load.atlasJSONHash('testbg', 'assets/img/newbgtest/testjson.png', 'assets/img/newbgtest/testjson.json');

    //Load your sounds, efx, music...
    //Example: game.load.audio('rockas', 'assets/snd/rockas.wav');
    this.load.audio('fireside', ['/assets/audio/music/fireside.mp3', 'WebContent/assets/audio/fireside.ogg']);
    this.load.audio('socold', '/assets/audio/music/socold.mp3');
    this.load.audio('divination', '/assets/audio/music/divination.mp3');
    this.load.audio('priscillas_song', '/assets/audio/music/priscillas_song.wav');
    this.load.audio('mmhover', ['/assets/audio/music/mmhover.ogg']);

    
    this.load.video('winterfold-intro', '/assets/video/winterfold.webm');

    //Load your data, JSON, Querys...
    //Example: game.load.json('version', 'http://phaser.io/version.json');
    this.load.spritesheet('tileset', 'assets/tilemaps/maps/Newtry/sprites.png', 32, 32);
    this.load.image('sprite', 'assets/sprites/sprite.png'); // this will be the sprite of the players

    this.load.tilemap('map', 'assets/tilemaps/maps/Newtry/TestLandiaV2.json', null, Phaser.Tilemap.TILED_JSON);

    this.load.spritesheet('player', '/assets/sprites/diagfull_1.png');
};

Winterfold.PreloaderState.prototype.create = function() {
    this.stage.backgroundColor = '#000';

    //A simple fade out effect
    this.time.events.add(Phaser.Timer.SECOND * 0.8, function () {
        var tween = this.add.tween(this.preloadBar)
            .to({ alpha: 0 }, 750, Phaser.Easing.Linear.none);

        tween.onComplete.add(function () {
            this.preloadBar.destroy();
            this.startGame();
        }, this);

        tween.start();
    }, this);

    this.startGame = function() {
        this.state.start('MovieState');
    }
}



