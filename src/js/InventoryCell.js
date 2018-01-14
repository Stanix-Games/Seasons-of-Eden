var InventoryCell = function(game, x, y, id, occupied, occupiedWith) {
    var self = this;

    // Make an extension to Phaser.
    Phaser.Sprite.call(self, game, 0, 0, id);


    self.anchor.setTo(0.5);
    self.ttt = {
        occupied: occupied,
        occupiedWith: occupiedWith
    };

    game.add.existing(self);

};

InventoryCell.prototype = Object.create(Phaser.Sprite.prototype);
InventoryCell.prototype.constructor = InventoryCell;