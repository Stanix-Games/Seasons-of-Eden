function inventory(game, x, y) {
    var inventory = {};

    inventory.iarray = [
        [0, 0, 0],
        [0, 0, 0],
        [0, 0, 0]
    ];

    // Loop through the inventory array and replace the
    // inventory cells accordingly.
    for (var ix = 0; ix < inventory.iarray.length;  ix++) {
        // Calculate the rows
        for (var iy = 0; iy < inventory.iarray[ix].length;  iy++) {
            var cell = new InventoryCell(game, x + (app.SQUARE_SIZE * ix), y + (app.SQUARE_SIZE * iy), 'grid' );
            cell.scale.setTo(3);
            cell.ttt.occupied = false;
            cell.ttt.occupiedWith = null;
            inventory.iarray[ix][iy] = cell;
        }
    }

    return inventory;
}

function FitCheck(startingPoint, itemSize) {
    for(var x = 0; x < itemSize.x; x++) {
        for (var y = 0; y < itemSize.y; y++) {
            if(x + startingPoint.x >= app.inventory.iarray.length || y + startingPoint.y >= app.inventory.iarray.length) {
                return false;
            } else if (app.inventory.iarray[x + startingPoint.x][y + startingPoint.y].ttt.occupied) {
                return false;
            }
        }
    }
    return true;
}

function FindStartingPoint(itemSize) {
    for(var x = 0; x < app.inventory.iarray.length; x++) {
        for (var y = 0; app.inventory.iarray[x].length; y++) {
            var cell = new Phaser.Point();
            cell.x = x;
            cell.y = y;

            if(FitCheck(cell, itemSize)) {
                return cell;
            }
        }
    }

    return null;
}

function AddItemToInventory(item) {
    var startingPoint = FindStartingPoint(item.ttt.inventorySize);

    if (startingPoint != null) {
        for (var x = 0; x < item.ttt.inventorySize.x; x++) {
            for (var y = 0; y < item.ttt.inventorySize.y; y++) {
                app.inventory.iarray[x+startingPoint.x][y + startingPoint.y].ttt.occupied = true;
                app.inventory.iarray[x+startingPoint.x][y + startingPoint.y].ttt.occupiedWith = item;
            }
        }
    item.ttt.spawn(
        app.inventory.iarray[startingPoint.x][startingPoint.y].x,
        app.inventory.iarray[startingPoint.x][startingPoint.y].y
    );
    } else {
        console.log("Inventory Full");
    }
    
}