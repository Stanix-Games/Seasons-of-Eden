// class Camera extends Phaser.Group {
//     constructor() {
//        ...
//         this.scale.setTo(1);
//        ...
//     }
//     zoomTo(scale) {
//         this.scale.setTo(scale)
//     }

// }
var Winterfold = Winterfold || {};

Winterfold.Camera = function () {
    "use strict";
    Phaser.Group.call(this.game);

    var {world, physics, camera} = this.game;
    var {centerX, centerY, bounds} = world;
    this.scale.setTo(1);
    this.position.setTo(this.x, this.y);
    this.bounds = Phaser.Rectangle.clone(this.game.world.bounds);

    this.player = this.add.sprite(0, 0, null);
    this.player.position.setTo(centerX, centerY);
    this.physics.arcade.enable(this.player);
    camera.follow(this.player);

    this.nullTarget = this.add.sprite(centerX, centerY, null);
    this.target = this.nullTarget;

    this.secondsToTarget = 0.5;
    this.safeDistance = 3;

    return this;
    
};

Winterfold.Camera.prototype.removeTarget = function() {
    this.target = this.nullTarget;
}

Winterfold.Camera.prototype.follow = function(target) {
    this.target = target.world;
};

Winterfold.Camera.prototype.getSpeed = function() {
    var distance = this.player.position.distance(this.target);
    if (distance < this.safeDistance) return 0;
    else return distnace / this.secondsToTarget;
}

// Copy all of the methods from Phaser Group to the Camera, similar to using Extends with Classes.
Winterfold.Camera.prototype = Object.create(Phaser.Group.prototype); 

// Retain the constructor of the prototype.
Winterfold.Camera.prototype.constructor = Winterfold.Camera;
Winterfold.Camera.prototype.constructor.x = 0;
Winterfold.Camera.prototype.constructor.y = 0;


Winterfold.Camera.prototype.zoomTo = function(scale) {
    // this.scale.setTo(scale);
    console.log(Winterfold.Camera.scale);
    var bounds       = this.bounds;
    // var cameraBounds = this.game.camera.bounds;
    // cameraBounds.x      = bounds.width  * (1 - scale) / 2;
    // cameraBounds.y      = bounds.height * (1 - scale) / 2;
    // cameraBounds.width  = bounds.width  * scale;
    // cameraBounds.height = bounds.height * scale;

    //  if (!duration) {
    // } else {
    //     this.game.add.tween(cameraBounds).to({
    //         x      : bounds.width  * (1 - scale) / 2,
    //         y      : bounds.height * (1 - scale) / 2,
    //         width  : bounds.width  * scale,
    //         height : bounds.height * scale
    //     }, duration).start();
    //     return this.game.add.tween(this.scale).to({
    //         x: scale, y: scale
    //     }, duration).start();
    // }
};

    