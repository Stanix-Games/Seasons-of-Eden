var Winterfold = Winterfold || {};

Winterfold.BootState = function(){
    "use strict";
    Phaser.State.call(this);
};

// Copy all of the methods from Phaser State to BootState.  
Winterfold.BootState.prototype = Object.create(Phaser.State.prototype);

// Retain the constructor of the prototype.
Winterfold.BootState.prototype.constructor = Winterfold.BootState;


Winterfold.BootState.prototype.init = function() {
    // Sets aspect ratio that can scale accordingly.
    this.scale.scaleMode = Phaser.ScaleManager.SHOW_ALL;

    // Center the game canvas horizintally and vertically.
    this.scale.pageAlignHorizontally = true;
    this.scale.pageAlignVertically = true;

    this.scale.setMaximum();

    this.stage.disableVisibilityChange = true;
};

Winterfold.BootState.prototype.preload = function() {
    // Load Preload Bar
    this.load.atlasJSONHash('preloadBar', '/assets/img/StanixIntro.png', '/assets/img/StanixIntro.json');
    this.load.atlasJSONHash('MMBackground', '/assets/img/MMBackground.png', '/assets/img/MMBackground.json');
};

Winterfold.BootState.prototype.create = function() {
    //Initial GameSystem (Arcade, P2, Ninja)
    Winterfold.game.physics.startSystem(Phaser.Physics.ARCADE);

    // Change BG color to White
    this.stage.backgroundColor = '#000';
    // Start Preload State
    this.state.start('PreloaderState');

    //HACK TO PRELOAD SUMIRE CUSTOM FONT
    this.add.text(0, 0, "hack", { font: "30px sumiremedium", fill: "#000" });

    
};

