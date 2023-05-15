using GXPEngine;
using System;

public class Vent : AnimationSprite
{
    public bool isActive = true;
    public Vec2 position;

    private float detectionRangeY = 500f;
    private float ventWidth = 100f;
    private float forcePower = -0.5f;

    public Vent(Vec2 _position) : base("flower-vent.png", 4,1)
    {
        position.x = _position.x;
        position.y = _position.y;

        MyGame myGame = (MyGame)game;

        myGame.vents.Add(this);
        x = position.x;
        y = position.y;
    }

    void Update() {
        if (!isActive) return;
        Animate(0.1f);
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
        float distanceY = Math.Abs(player.position.y - position.y);
        return distanceY <= detectionRangeY;
    }
}