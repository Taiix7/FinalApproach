using GXPEngine;
using System;
using TiledMapParser;

class Vent : Sprite
{
    public bool isActive = true;
    public Vec2 position;

    private float detectionRangeY = 200f;
    private float ventWidth = 200f;
    private float floatForce = 20f;
    private float floatFrequency = 2f;
    private float floatAmplitude = 10f;
    
    public Vent(Vec2 pPosition, TiledObject obj = null) : base("circle.png")
    {
        position = pPosition;
        MyGame myGame = (MyGame)game;
        myGame.AddChild(this);

        x = position.x;
        y = position.y;
    }

    public void ApplyVentEffect(Player player)
    {
        float distanceY = Math.Abs(player.position.y - position.y);
        float distanceX = Math.Abs(player.position.x - position.x);

        if (isActive && distanceY <= ventWidth && distanceX <= ventWidth)
        {
            float floatOffset = (float)Math.Sin(Time.time * floatFrequency) * floatAmplitude;
            float floatForceMagnitude = floatForce * (1f - (float)Math.Abs(player.position.y - position.y) / ventWidth);
            Vec2 floatForceVector = new Vec2(0, -floatForceMagnitude) * player.mass;

            player.ApplyForce(floatForceVector);
        }

    }

    public bool IsPlayerInRange(Player player)
    {
        float distanceY = Math.Abs(player.position.y - position.y);
        return distanceY <= detectionRangeY;
    }
}

