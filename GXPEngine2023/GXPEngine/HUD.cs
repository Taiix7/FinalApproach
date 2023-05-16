using System;
using System.Drawing;
using GXPEngine;

class HUD : GameObject
{
    EasyDraw distance;

    EasyDraw timer;

    String text = "";
    Player player;
    //public float time = 4;

    public float timeLeft;

    private Sprite timerSprite;
    public float time;
    
    private Font font = Utils.LoadFont("Celtic.ttf", 40);


    public HUD(Player player)
    {

        timerSprite = new Sprite("timer.png");
        AddChild(timerSprite);

        this.player = player;
        distance = new EasyDraw(250, 60);
        distance.SetXY(0, 0);
        distance.Fill(Color.White);
        distance.TextSize(15);
        AddChild(distance);
        distance.collider.isTrigger = false;

        timer = new EasyDraw(250, 60);
        timer.SetXY(45,25);
        timer.Fill(Color.White);
        timer.TextSize(25);
        AddChild(timer);

        font = Utils.LoadFont("Celtic.ttf", 22);
        timer.TextFont(font);
    }

    //public void StickToCeiling()
    //{
    //    time -= 0.005f;
    //    if(((int)time) <= 0)
    //    {
    //        player.stickToWall = false;
    //        time = 4;
    //    }
    //    if (player != null)
    //    {
    //        distance.Clear(Color.Transparent);
    //        distance.Text(String.Format(text + ((int)time)));
    //        distance.SetXY(player.x - 15, player.y - 100);
    //    }
    //    Console.WriteLine(time);
    //}

    public void Timer()
    {
        timeLeft -= Time.deltaTime / 1000f;

        float min = Mathf.Floor(timeLeft / 60f);
        float sec = Mathf.Floor(timeLeft % 60f);

        timer.Clear(Color.Transparent);
        timer.Text(String.Format("{0:00}:{1:00}", min, sec));
    }
}
