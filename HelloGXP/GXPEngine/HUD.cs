using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GXPEngine;

public class HUD : GameObject
{
    EasyDraw movesCounter;
    EasyDraw collectables;
    Player player;

    EasyDraw rect = new EasyDraw(250, 60);
    EasyDraw rect2 = new EasyDraw(250, 60);

    public HUD(Player player){

        rect.Rect(70, 50, 125, 22);
        AddChild(rect);

        rect2.Rect(100, 0, 160, 50);
        rect2.SetXY(980, 35);
        AddChild(rect2);

        this.player = player;
        movesCounter = new EasyDraw(250, 60);
        movesCounter.Fill(Color.Blue);
        movesCounter.SetXY(10, 0);
        AddChild(movesCounter);

        collectables = new EasyDraw(250, 60);
        collectables.Fill(Color.Blue);
        collectables.SetXY(1000, 0);
        AddChild(collectables);


    }

    public void Check() {
        if (player != null) {
            movesCounter.Clear(Color.Transparent);
            movesCounter.Text(String.Format("Moves left: " + player.movesLeft));

            collectables.Clear(Color.Transparent);
            collectables.Text(String.Format("Collectables: {0} / 9", ((MyGame)game).collectables)) ;
        }
    }
}
