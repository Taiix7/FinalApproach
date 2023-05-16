using System;
using System.Drawing;
using GXPEngine;

class HUD : GameObject
{
    EasyDraw distance;

    EasyDraw timer;

    Player player;

    public float timeLeft;

    private Sprite timerSprite;

    private Font font = Utils.LoadFont("Celtic.ttf", 40);


    public HUD(Player player)
    {

        timerSprite = new Sprite("timer.png");
        timerSprite.SetXY(1650, 0);
        AddChild(timerSprite);


        this.player = player;

        timer = new EasyDraw(250, 60);
        timer.SetXY(1715, 25);
        timer.Fill(Color.White);
        timer.TextSize(25);
        AddChild(timer);

        font = Utils.LoadFont("Celtic.ttf", 22);
        timer.TextFont(font);
    }

    public void Timer()
    {
        timeLeft -= Time.deltaTime / 1000f;

        float min = Mathf.Floor(timeLeft / 60f);
        float sec = Mathf.Floor(timeLeft % 60f);

        timer.Clear(Color.Transparent);
        timer.Text(String.Format("{0:00}:{1:00}", min, sec));

        if (timeLeft <= 0)
        {
            if (player != null)
                player.Dead();
        }
    }
}
