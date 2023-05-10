using GXPEngine;
using TiledMapParser;

class LevelLine : GameObject
{
    NLineSegment line;

    public LevelLine(TiledObject obj = null) : base()
    {
        line = new NLineSegment(obj.X, obj.Y, 0xffffffff, 3);
        MyGame myGame = (MyGame)game;
        myGame.AddChild(line);
    }
}

