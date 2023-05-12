using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using System.Collections.Generic;

public class MyGame : Game
{
    public Vec2 deltaVec = new Vec2();
    public Vec2 empLines;
    public List<NLineSegment> list= new List<NLineSegment>();
    public List<Spike> spikes = new List<Spike>();

    string level = "level2.tmx";
    string nextlevel = null;
    private Player player;

    private Lever lever;
    private Level _level;
    private ResponsiveObject responsiveObject;

    public MyGame() : base(1920, 1080, false)     // Create a window that's 800x600 and NOT fullscreen
    {
        OnAfterStep += CheckLoadLevel;
        LoadLevel(level);
        CreateLevel();
    }

    private void CreateLevel()
    {
        //player = new Player(20, new Vec2(150, 700));

        responsiveObject = new ResponsiveObject(30, new Vec2(10,10));
        lever = new Lever(30, new Vec2(400, 300), responsiveObject);


        //AddChild(player);
        AddChild(responsiveObject);
        AddChild(lever);
    }

    // For every game object, Update is called every frame, by the engine:
    void Update()
    {
        if (_level == null) return;
        _level.player.Step();
        if (lever.IsMouseOver() && Input.GetMouseButtonDown(0))
        {
            lever.connectedObject.UpdateColor(152,242,0);
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

    public void Collisions(Vec2 deltaVec, Vec2 line, List<NLineSegment> linesList, Type type)
    {
        if (type == typeof(Player))
        {
            foreach (NLineSegment lines in linesList)
            {
                deltaVec = lines.start - player.position;
                line = lines.start - lines.end;

                float ballDistance = deltaVec.Dot(line.Normal());

                if (ballDistance - player.radius < 0)
                {

                    float a = ballDistance - player.radius;

                    player.position -= Mathf.Abs(a) * line.Normal();

                    player.velocity.Reflect(line.Normal(), 0.8f);
                    player.velocity *= .89f;
                }
                player.UpdateScreenPosition();

            }
        }
    }

        static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}