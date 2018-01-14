var Item = function(game, id, frame, name, damage, description, inventorySize) {
    var self = this;

    // Make an extension to Phaser.
    Phaser.Sprite.call(this, game, 0, 0, id);

    self.frame = frame;
    self.ttt = {
        name: name,
        damage: damage,
        description: description,
        inventorySize: inventorySize
    };

    self.visible = false;
    self.anchor.setTo(0.5);

    self.ttt.spawn = function(x, y) {
        self.visible = true;

        //Repositions the sprite at a given position.
        self.reset(x, y); 
    };

    self.scale.setTo(inventorySize.x, inventorySize.y);

    game.add.existing(this);
};

Item.prototype = Object.create(Phaser.Sprite.prototype);
Item.prototype.constructor = Item;