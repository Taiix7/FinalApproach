using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Collections.Generic;
using System.Threading;
using TiledMapParser;
using System.Reflection.Emit;

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
    public List<NLineSegment> ceillings = new List<NLineSegment>();
    public List<Button> buttons = new List<Button>();

    public string level = "opening.tmx";
    string nextlevel = null;

    private Level _level;

    //Sounds
    private SoundChannel channel;
    private Sound level1;
    private Sound level2;
    private Sound level3;

    public int levelNum;


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
        if(_level.player != null) { _level.player.Step(); }
        _level.hud.Timer();
        foreach (Vent vent in vents)
        {
            if (vent.IsPlayerInRange(_level.player))
            {
                vent.ApplyVentEffect(_level.player);
            }
        }

        if(level == "opening.tmx")
        {
            if(Input.GetMouseButtonUp(0))
            {
                level = "level_1_final.tmx";
                LoadLevel(level);
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
        foreach(NLineSegment ceillings in ceillings)
        {
            ceillings.Destroy();
        }
        foreach (GameObject objectives in objectives)
        {
            objectives.Destroy();
        }
        foreach (Button button in buttons)
        {
            button.Destroy();
        }
        list.Clear();
        spikes.Clear();
        ceillings.Clear();
        objectives.Clear();
        buttons.Clear();
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
                case "level_1_final.tmx":
                    if (channel != null)
                        channel.Stop();

                    channel = level1.Play();
                    _level.hud.timeLeft = 120f;
                    break;
                case "level_2_real_final.tmx":
                    channel.Stop();
                    channel = level2.Play();
                    _level.hud.timeLeft = 180f;
                    break;
                case "level_3.tmx":
                    channel.Stop();
                    channel = level3.Play();
                    if (_level.player == null)
                    _level.hud.LateDestroy();
                    break;
                default:
                    if (_level.player == null)
                        _level.hud.LateDestroy(); 
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