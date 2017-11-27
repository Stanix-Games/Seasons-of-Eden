export default class Game {
    create() {
        this.add.sprite(0, 0, "game_bg"); 
        this.clouds = this.add.group();
        this.score = 0;
        let style = { 
            font: "24px Arial", 
            fill: "#FFFFFF"
        };
        this.txtScore = this.add.text(10, 10, this.score.toString(), style); 
    }
    update() {
        if (Math.random() < .01) {
            let cloud = this.clouds.getFirstDead(); if (cloud) {
                cloud.x = Math.random() * this.game.width;
                cloud.y = Math.random() * this.game.height;
                cloud.revive();
            } else {
                let cloud = this.clouds.create(Math.random() * this.game.width, Math.random() * this.game.height, "cloud");
                cloud.inputEnabled = true; cloud.events.onInputDown.add(this.
                    onCloudClick, this);
            }
            cloud.alpha = 0; this.add.tween(cloud).to({ 
                y: "−50", 
                alpha: 1 
            }, 800, Phaser.Easing.Cubic.Out, true);
        } 
    }
    onCloudClick(cloud) {
        cloud.kill();
        this.score++;
        this.txtScore.setText(this.score.toString());
    }
}
