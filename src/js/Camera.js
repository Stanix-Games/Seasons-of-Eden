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
    Phaser.State.call(this);
    this.x = 0;
    this.y = 0;
    // this.scale.setTo(1);

    // this.bounds = Phaser.Rectangle.clone(game.world.bounds);

};

// Copy all of the methods from Phaser Group to the Camera, similar to using Extends with Classes.
Winterfold.Camera.prototype = Object.create(Phaser.Group.prototype); 

// Retain the constructor of the prototype.
Winterfold.Camera.prototype.constructor = Winterfold.Camera;


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

    