﻿using GXPEngine;
using TiledMapParser;

public class CeillingLine : Sprite
{
    NLineSegment lineCeilling;

    public CeillingLine(TiledObject obj = null) : base("Empty.png")
    {
        lineCeilling = new NLineSegment(obj.X, obj.Y, obj.X + obj.Width, obj.Y, 0xffffffff, 3);
        MyGame myGame = (MyGame)game;
        myGame.ceillings.Add(lineCeilling);
    }
}