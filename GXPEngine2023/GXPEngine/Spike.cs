using GXPEngine;
using TiledMapParser;

public class Spike : Sprite
{
    public int radius = 10;
    public Vec2 position;

    public Spike(TiledObject obj = null) : base("Empty.png")
    {
        position.x = obj.X;
        position.y = obj.Y;
        x = position.x;
        y = position.y;
        SetOrigin(radius, radius);
        MyGame myGame = (MyGame)game;
        myGame.spikes.Add(this);
    }
}
