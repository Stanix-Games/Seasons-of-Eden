export default class Preload {
    constructor() { 
        this.asset = null; 
        this.ready = false; 
    }
    preload() {
        this.load.image('loading_bg', '/Users/Christopher/Repository/Seasons-of-Eden/lib/assets/images/loading_bg.jpg');
    }
    create() {
        //background for game
        this.add.sprite(0, 0, "loading_bg");
        this.asset = this.add.sprite(this.game.width / 2, this.game.height / 2, 'preloader');
        this.asset.anchor.setTo(0.5, 0.5);
        this.load.onLoadComplete.addOnce(this.onLoadComplete, this);
        this.load.setPreloadSprite(this.asset);

        //do all your loading here 
        this.load.image('player', 'assets/images/player.png'); 
        this.load.image('cloud', 'assets/images/cloud.png');
        this.load.image('game_bg', '/Users/Christopher/Repository/Seasons-of-Eden/lib/assets/images/game_bg.jpg');
        //width and height of sprite
        //staaaart load
        this.load.start();
    }
    update() {
        if (this.ready) {
            this.game.state.start('game');
        }
    }
    onLoadComplete() {
    this.ready = true;
    }
}