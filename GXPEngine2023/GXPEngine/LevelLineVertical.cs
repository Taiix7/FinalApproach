using GXPEngine;
using TiledMapParser;

class LevelLineVertical : Sprite
{
    NLineSegment lineVertical;

    public LevelLineVertical(TiledObject obj = null) : base("Empty.png")
    {
        lineVertical = new NLineSegment(obj.X + obj.Width, obj.Y, obj.X + obj.Width, obj.Y + obj.Height, 0xffffffff, 3);
        MyGame myGame = (MyGame)game;
        myGame.list.Add(lineVertical);
        myGame.AddChild(lineVertical);
    }
}