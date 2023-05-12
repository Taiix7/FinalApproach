using GXPEngine;
using System;
using System.Threading;
using TiledMapParser;

public class Vent : Sprite
{
    public bool isActive = true;
    public Vec2 position;

    private float detectionRangeY = 200f;
    private float ventWidth = 100f;
    private float floatForce = 50f;
    private float floatFrequency = 2f;
    private float floatAmplitude = 10f;
    
    public Vent(TiledObject obj = null) : base("circle.png")
    {
        position.x = obj.X;
        position.y = obj.Y;

        MyGame myGame = (MyGame)game;

        myGame.vents.Add(this);
        x = position.x;
        y = position.y;
    }

    public void ApplyVentEffect(Player player)
    {
        float distanceY = Math.Abs(player.position.y - position.y);
        float distanceX = Math.Abs(player.position.x - position.x);

        if (isActive && distanceY <= ventWidth && distanceX <= ventWidth)
        {
            //float floatOffset = (float)Math.Sin(Time.time * floatFrequency) * floatAmplitude;
            //float floatForceMagnitude = floatForce * (1f - (float)Math.Abs(player.position.y - position.y) / ventWidth);
            //Vec2 floatForceVector = new Vec2(0, -floatForceMagnitude * floatOffset * player.mass);

            //player.position = new Vec2((float)Math.Sin(player.position.x, 0), 0);
            //player.position += player.x * Mathf.Sin(Time.time * 3f + 1f) * 1f;
        }
    

    }

    public bool IsPlayerInRange(Player player)
    {
        float distanceY = Math.Abs(player.position.y - position.y);
        return distanceY <= detectionRangeY;
    }
}

