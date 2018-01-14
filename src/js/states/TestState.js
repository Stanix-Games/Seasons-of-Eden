var Winterfold = Winterfold || {};

Winterfold.TestState = function () {
	"use strict";
	Phaser.State.call(this);
};

// Copy all of the methods from Phaser State to TestState. 
Winterfold.TestState.prototype = Object.create(Phaser.State.prototype);

// Retain the constructor of the prototype.
Winterfold.TestState.prototype.constructor = Winterfold.TestState;

Winterfold.TestState.prototype.preload = function () {
	// Loading Font Style
	this.style = {
		font: "48px sumiremedium",
		fill: "#fff",
		boundsAlignH: "center",
		boundsAlignV: "middle"
	};

};

Winterfold.TestState.prototype.create = function () {
	var self = this;
	console.log(self.world);
	this.inv = inventory(self.game, this.world.centerX, this.world.centerY);
	// app.inventory = this.inv; // This makes inventory a global variable, so that it can be used in other places of the codebase.

	// // 1x2 Items
	// for (var i = 0; i < 2; i++) {
	// 	var item = new Item(this, 'items', 1, 'Sword', 1, 'Very Pointy', new Phaser.Point(1, 2));
	// 	AddItemToInventory(item);
	// }
	// // 1x1 Items
	// for (var i = 0; i < 3; i++) {
	// 	var item = new Item(this, 'items', 0, 'Test Tube', 0, 'Empty', new Phaser.Point(1, 1));
	// 	AddItemToInventory(item);
	// }
};

Winterfold.TestState.prototype.update = function () {

};