var Winterfold = Winterfold || {};
Winterfold.MovieState = function(){
    "use strict";
    Phaser.State.call(this);
};

// Copy all of the methods from Phaser State to MovieState. 
Winterfold.MovieState.prototype = Object.create(Phaser.State.prototype);

// Retain the constructor of the prototype.
Winterfold.MovieState.prototype.constructor = Winterfold.MovieState;

Winterfold.MovieState.prototype.preload = function() {
        this.stage.backgroundColor = '#000';
        var video = Winterfold.game.add.video('winterfold-intro');

        video.play(true);

        var videoImage = video.addToWorld(this.world.centerX, this.world.centerY, 0.5, 0.5);
        videoImage.width = window.innerWidth * window.devicePixelRatio;
        videoImage.height = window.innerHeight * window.devicePixelRatio;
        var self = this;
        this.nextState = function() {
            self.time.events.add(1000, function() {
                videoImage.destroy();
                video.destroy();
                self.state.start('TitleState');
            });
        }
        
        this.input.onDown.add(this.nextState, this);  
}

