var Winterfold = Winterfold || {};

Winterfold.TitleState = function () {
    "use strict";
    Phaser.State.call(this);
};

// Copy all of the methods from Phaser State to TitleState. 
Winterfold.TitleState.prototype = Object.create(Phaser.State.prototype);

// Retain the constructor of the prototype.
Winterfold.TitleState.prototype.constructor = Winterfold.TitleState;

Winterfold.TitleState.prototype.preload = function() {
    // Loading Font Style
    this.style = {
        font: "48px sumiremedium",
        fill: "#fff",
        boundsAlignH: "center",
        boundsAlignV: "middle"
    };

    this.bgtest = this.add.sprite(0, 0, 'bgtest');
    this.bgtest.width = window.innerWidth * window.devicePixelRatio;
    this.bgtest.height = window.innerHeight * window.devicePixelRatio;

    // Winterfold Logo
    this.winterfoldLogo = this.add.sprite(this.world.width * 0.10, this.world.height * 0.15, 'winterfold-logo');
    this.winterfoldLogo.scale.setTo(1.5);

    // New BG Test
    this.testbg = this.add.sprite(0, 0, 'testbg');
    this.testbg.width = window.innerWidth * window.devicePixelRatio;
    this.testbg.height = window.innerHeight * window.devicePixelRatio;
    this.testbg.animations.add('bganimation');
    this.testbg.play('bganimation', 5, true);
}

Winterfold.TitleState.prototype.create = function() {

    this.add.plugin(Phaser.Plugin.Debug);
    this.add.plugin(Phaser.Plugin.Inspector);
    this.add.plugin(PhaserSuperStorage.StoragePlugin);
    this.add.plugin(PhaserInput.Plugin);

    // Music
    this.music = this.add.audio('priscillas_song');
    this.music.play();
    this.music.loop = true;
    this.hoversound = this.add.audio('mmhover');
    this.hoversound.volume = 100;
    this.options = this.add.group();
    this.options.inputEnabled = true;
    

    var cont = this.add.text(this.world.width * 0.20, this.world.height * 0.35, "Continue", this.style, this.options);
    cont.setShadow(3, 3, 'rgba(0,0,0,0.5)', 2);
    cont.alpha = 0.3;
    var newGame = this.add.text(this.world.width * 0.20, this.world.height * 0.40, "New", this.style, this.options);
    newGame.setShadow(3, 3, 'rgba(0,0,0,0.5)', 2);
    var loadGame = this.add.text(this.world.width * 0.20, this.world.height * 0.45, "Load", this.style, this.options);
    loadGame.setShadow(3, 3, 'rgba(0,0,0,0.5)', 2);
    var credits = this.add.text(this.world.width * 0.20, this.world.height * 0.50, "Credits", this.style, this.options);
    credits.setShadow(3, 3, 'rgba(0,0,0,0.5)', 2);

    console.log(this.options.children);
};

Winterfold.TitleState.prototype.update = function() {
    var self = this;
    for (let option of this.options.children) {
        option.inputEnabled = true;
        option.events.onInputOver.addOnce(over, self);
        option.events.onInputOut.addOnce(out, self);
        option.events.onInputDown.addOnce(down, self);
    }
    // this.options.children.forEach(function(option) {

    // });

};

function over(item, self) {
    if (item._text !== 'Continue' && item._text !== undefined) {
        item.setText(item._text);
        item.style.fill = "#a51411";
    }
    debugger;
    console.log(self);
    console.log(item);
}

function out(item) {
    if (item._text !== 'Continue') {
        item.setText(item._text);
        item.style.fill = '#fff';
    }
}

function down(item) {
    if (item._text === 'Continue') {
        this.music.destroy();
        this.state.start('TestState');
    }
}