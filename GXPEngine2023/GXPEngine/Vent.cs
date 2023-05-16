using GXPEngine;
using System;

public class Vent : AnimationSprite
{
    public bool isActive = true;
    public Vec2 position;

    private float detectionRangeY = 500f;
    private float ventWidth = 50f;
    private float forcePower = 0.5f;

    private Sound ventSound;
    private SoundChannel ch;
    private AnimationSprite wind;
    private AnimationSprite wind1;

    public Vent(Vec2 _position, float forcePower) : base("flower-vent.png", 4,1)
    {
        wind = new AnimationSprite("wind-animation-spritesheet.png", 6, 1);
        wind.SetXY(-25, -230);
        wind.width = 150;
        wind.height = 250;
        AddChild(wind);
        wind1 = new AnimationSprite("wind-animation-spritesheet.png", 6, 1);
        wind1.SetXY(-25, -470);
        wind1.width = 150;
        wind1.height = 250;
        AddChild(wind1);
        position.x = _position.x;
        position.y = _position.y;
        this.forcePower = forcePower;

        MyGame myGame = (MyGame)game;

        myGame.vents.Add(this);
        x = position.x;
        y = position.y;

        
        ventSound = new Sound("vent_up.wav", true, false);
        ch = ventSound.Play();
    }

    void Update() {
        if (!isActive) { ch.Stop(); wind.visible=false; wind1.visible = false; return;}
        Animate(0.1f);
        wind.visible= true;
        wind.Animate(0.1f);
        wind1.visible = true;
        wind1.Animate(0.1f);
    }

    public void ApplyVentEffect(Player player)
    {
        float distanceY = Math.Abs(player.position.y - position.y);
        float distanceX = Math.Abs(player.position.x - position.x);

        if (isActive && distanceY <= detectionRangeY && distanceX <= ventWidth)
        {
            player.acceleration += new Vec2(0, forcePower);
        }
    }

    public bool IsPlayerInRange(Player player)
    {
        if (player == null) return false;
        float distanceY = Math.Abs(player.position.y - position.y);
        return distanceY <= detectionRangeY;
    }
}