using GXPEngine;
using TiledMapParser;


public class Objective : Sprite
{
    public int radius = 20;
    public Vec2 position;
    public Objective(TiledObject obj = null) : base("End_Idle.png")
    {
        position.x = obj.X;
        position.y = obj.Y;
        x = position.x;
        y = position.y;
        SetOrigin(radius, radius);
        MyGame myGame = (MyGame)game;
        myGame.objectives.Add(this);
    }
}