using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Collections.Generic;
using System.Threading;
using TiledMapParser;

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

    private float time;

    //Sounds
    private SoundChannel channel;
    private Sound level1;
    private Sound level2;
    private Sound level3;


    public MyGame() : base(1920, 1080, false)     // Create a window that's 800x600 and NOT fullscreen
    {
        level1 = new Sound("lvl1song.wav", true);
        level2 = new Sound("lvl2song.wav", true);
        level3 = new Sound("lvl3song.wav", true);

        OnAfterStep += CheckLoadLevel;
        LoadLevel(level);
    }

    void Update()
    {
        if (_level == null) return;
        _level.player.Step();
        Timer();

        foreach (Vent vent in vents)
        {
            if (vent.IsPlayerInRange(_level.player))
            {
                vent.ApplyVentEffect(_level.player);
            }
        }
    }

    public void LoadLevel(string levelName)
    {
        nextlevel = levelName;
        
        switch (levelName) {
            case "level_1.tmx":
                channel = level1.Play();
                    break;
            case "level_2.tmx":
                channel.Stop();
                channel = level2.Play();
                break;
            case "level_3.tmx":
                channel.Stop();
                channel = level3.Play();
                break;
        }
    }

    void DestroyLevel()
    {
        List<GameObject> children = GetChildren();
        foreach (GameObject child in children)
        {
            child.Destroy();
        }
        foreach (NLineSegment lines in list)
        {
            lines.Destroy();
        }
        foreach(Spike spike in spikes)
        {
            spike.Destroy();
        }
        list.Clear();
        spikes.Clear();
    }

    public void CheckLoadLevel()
    {
        if (nextlevel != null)
        {
            DestroyLevel();
            _level = new Level(nextlevel);
            AddChild(_level);
            nextlevel = null;
        }
    }

    void Timer()
    {
        time = Time.time / 1000;
        int min = (int)Math.Floor(time / 60);
        int sec = (int)Math.Floor(time % 60);
    }


    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}