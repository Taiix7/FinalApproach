using GXPEngine;
using System;
using TiledMapParser;

class LevelLine : Sprite
{
    NLineSegment line;

    public LevelLine(TiledObject obj = null) : base("circle.png")
    {
        line = new NLineSegment(obj.X, obj.Y,obj.X + obj.Width, obj.Y, 0xffffffff, 3);
        MyGame myGame = (MyGame)game;
        myGame.list.Add(line);
    }
}

