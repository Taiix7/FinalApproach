using System;
using System.Drawing;
using System.Threading;
using GXPEngine;

class HUD : GameObject
{
    EasyDraw distance;
    String text = "";
    Player player;
    public float time = 4;

    public HUD(Player player)
    {
        this.player = player;
        distance = new EasyDraw(250, 60);
        distance.SetXY(0, 0);
        distance.Fill(Color.White);
        distance.TextSize(15);
        AddChild(distance);
        distance.collider.isTrigger = true;
    }

    public void StickToCeiling()
    {
        time -= 0.005f;
        if(((int)time) <= 0)
        {
            player.stickToWall = false;
            time = 4;
        }
        if (player != null)
        {
            distance.Clear(Color.Transparent);
            distance.Text(String.Format(text + ((int)time)));
            distance.SetXY(player.x - 15, player.y - 100);
        }
        Console.WriteLine(time);
    }
}
