// =============================================================================
// SETUP
// =============================================================================

require('dotenv').config(); // npm install --save dotenv

// =============================================================================
// MIDDLEWARE
// =============================================================================

var express = require('express');
var app = express();
var server = require('http').Server(app);
var io = require('socket.io').listen(server);
var requirejs = require('requirejs');


// =============================================================================
// LOAD DIRECTORIES
// =============================================================================


app.use('/css', express.static(__dirname + '/css'));
app.use('/js', express.static(__dirname + '/js'));
app.use('/assets', express.static(__dirname + '/assets'));
app.use('/lib', express.static(__dirname + '/lib'));
app.use('/plugins', express.static(__dirname + '/plugins'));

// =============================================================================
// RENDER STATIC PAGE
// =============================================================================

app.get('/', function (req, res) {
    res.sendFile(__dirname + '/index.html');
});


app.get('/', function (req, res) {
    res.sendFile(__dirname + '/index.html');
});

// =============================================================================
// SOCKET.IO 
// =============================================================================
server.lastPlayderID = 0; // Keep track of the last id assigned to a new player

io.on('connection', function (socket) {
    socket.on('A new player has joined the zone.', function () {
        socket.player = {
            id: server.lastPlayderID++,
            x: randomInt(100, 400),
            y: randomInt(100, 400)
        };
        // Send to the new player the list of already connected players
        socket.emit('allplayers', getAllPlayers());
        socket.broadcast.emit('A new player has joined the zone.', socket.player);
    });
});

function getAllPlayers() {
    // This is an array of all of the current connected players
    var players = [];
    Object.keys(io.sockets.connected).forEach(function (socketID) {
        var player = io.sockets.connected[socketID].player;
        if (player) players.push(player);
    });
    return players;
}

function randomInt(low, high) {
    return Math.floor(Math.random() * (high - low) + low);
}

// =============================================================================
// LAUNCH SERVER ON DESIGNATED PORT
// =============================================================================

server.listen(process.env.PORT, () => console.log(`Launching Seasons of Eden on port ${process.env.PORT}`));