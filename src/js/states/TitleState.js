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
}

Winterfold.TitleState.prototype.create = function() {

    this.add.plugin(Phaser.Plugin.Debug);
    this.add.plugin(Phaser.Plugin.Inspector);
    this.add.plugin(PhaserSuperStorage.StoragePlugin);
    this.add.plugin(PhaserInput.Plugin);

    // Music
    this.music = this.add.audio('divination');
    this.music.play();
    this.music.volume = 0.2;
    this.hoversound = this.add.audio('mmhover');
    this.hoversound.volume = 100;
    this.options = this.add.group();
    this.options.inputEnabled = true;
    

    var cont = this.add.text(this.world.width * 0.10, this.world.height * 0.35, "Continue", this.style, this.options);
    cont.setShadow(3, 3, 'rgba(0,0,0,0.5)', 2);
    cont.alpha = 0.3;
    var newGame = this.add.text(this.world.width * 0.10, this.world.height * 0.40, "New", this.style, this.options);
    newGame.setShadow(3, 3, 'rgba(0,0,0,0.5)', 2);
    var loadGame = this.add.text(this.world.width * 0.10, this.world.height * 0.45, "Load", this.style, this.options);
    loadGame.setShadow(3, 3, 'rgba(0,0,0,0.5)', 2);
    var credits = this.add.text(this.world.width * 0.10, this.world.height * 0.50, "Credits", this.style, this.options);
    credits.setShadow(3, 3, 'rgba(0,0,0,0.5)', 2);

    console.log(this.options.children);
};

Winterfold.TitleState.prototype.update = function() {
    var self = this;
    this.options.children.forEach(function(option) {
        option.inputEnabled = true;
        option.events.onInputOver.add(over, self);
        option.events.onInputOut.add(out, self);
        option.events.onInputDown.add(down, self);
    });

    // self.options.children.forEach(function(option) { 
    //     Winterfold.game.time.events.add(1000, function () {
    //         option.events.onInputOver.add(function () {
    //             if (option._text !== 'Continue') {
    //                 option.setText(option._text);
    //                 option.style.fill = "#a51411";
    //                 console.log(self.options.children[0]._text);
    //             }

    //             if (!self.isPlaying) {
    //                 Winterfold.TitleState.hoversound.fadeIn(1000);
    //                 this.isPlaying = true;
    //             }
    //         }, self);
    //     });

    //     option.events.onInputOut.add(function () {
    //         if (option._text !== 'Continue') {
    //             option.setText(option._text);
    //             option.style.fill = '#fff';
    //         }
    //         if (self.isPlaying) {
    //             Winterfold.TitleState.hoversound.fadeOut(1000);
    //             self.isPlaying = false;
    //         }
    //     }, self);

    //     Winterfold.game.time.events.add(1000, function () {
    //         option.events.onInputDown.add(function () {
    //             if (option._text === 'Continue') {
    //                 self.music.destroy();
    //                 self.game.state.start('TestState');
    //             }
    //         });
    //     });
    // });

};

function over(item, self) {
    if (item._text !== 'Continue' && item._text !== undefined) {
        item.setText(item._text);
        item.style.fill = "#a51411";
    }

    this.hoversound.fadeIn(1000);
    

    // if (!self.isPlaying) {
    //     this.hoversound.fadeIn(1000);
    //     self.isPlaying = true;
    // }
}

function out(item) {
    if (item._text !== 'Continue') {
        item.setText(item._text);
        item.style.fill = '#fff';
    }

    this.hoversound.fadeOut(1000);
    // if (self.isPlaying) {
    //     Winterfold.TitleState.hoversound.fadeOut(1000);
    //     self.isPlaying = false;
    // }
}

function down(item) {
    if (item._text === 'Continue') {
        this.music.destroy();
        this.state.start('TestState');
    }
}