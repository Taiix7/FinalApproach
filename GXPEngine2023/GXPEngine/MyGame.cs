using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Collections.Generic;
using System.Threading;
using TiledMapParser;

public class MyGame : Game
{
    public Vec2 deltaVec = new Vec2();
    public Vec2 empLines;

    public string currentLevel;

    public List<NLineSegment> list = new List<NLineSegment>();
    public List<Spike> spikes = new List<Spike>();
    public List<Lever> levers = new List<Lever>();
    public List<Vent> vents = new List<Vent>();
    public List<Objective> objectives = new List<Objective>();

    string level = "level_1.tmx";
    string nextlevel = null;

    private Level _level;

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
        _level.hud.Timer();
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
        foreach (Spike spike in spikes)
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

            currentLevel = nextlevel;
            DestroyLevel();
            _level = new Level(nextlevel);
            AddChild(_level);

            switch (nextlevel)
            {
                case "level_1.tmx":
                    if (channel != null)
                        channel.Stop();

                    channel = level1.Play();
                    _level.hud.timeLeft = 120f;
                    break;
                case "level_2_real.tmx":
                    channel.Stop();
                    channel = level2.Play();
                    _level.hud.timeLeft = 180f;
                    break;
                case "level_3.tmx":
                    channel.Stop();
                    channel = level3.Play();
                    _level.hud.timeLeft = 300f;
                    break;
            }

            nextlevel = null;
        }
    }

    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}