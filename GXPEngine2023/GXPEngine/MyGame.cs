using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using System.Collections.Generic;

public class MyGame : Game
{
    public Vec2 deltaVec = new Vec2();
    public Vec2 empLines;

    public List<NLineSegment> list = new List<NLineSegment>();
    public List<Spike> spikes = new List<Spike>();
    public List<Lever> levers = new List<Lever>();
    public List<Vent> vents = new List<Vent>();
    public List<Objective> objectives = new List<Objective>();

    string level = "level_1.tmx";
    string nextlevel = null;

    private Level _level;

    public MyGame() : base(1920, 1080, false, true, 5760, 3240, true)     // Create a window that's 800x600 and NOT fullscreen
    {
        OnAfterStep += CheckLoadLevel;
        LoadLevel(level);
    }

    void Update()
    {
        if (_level == null) return;
        _level.player.Step();

        foreach (Vent vent in vents)
        {
            if (vent.IsPlayerInRange(_level.player)) {
                vent.ApplyVentEffect(_level.player);
            }
        }
    }

    public void LoadLevel(string levelName)
    {
        nextlevel = levelName;
    }

    void CheckLoadLevel()
    {
        if (nextlevel != null)
        {
            _level = new Level(nextlevel);

            //DestroyAll();
            AddChild(_level);
            nextlevel = null;
        }
    }


    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}