using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using System.Collections.Generic;

public class MyGame : Game
{
    public Vec2 deltaVec = new Vec2();
    public Vec2 empLines;
    public List<NLineSegment> list= new List<NLineSegment>();

    string level = "Level1.tmx";
    string nextlevel = null;
    private Player player;

    private Lever lever;
    private ResponsiveObject responsiveObject;
    LevelLine line;

    public MyGame() : base(1920, 1080, false)     // Create a window that's 800x600 and NOT fullscreen
    {
        //line = new LevelLine();
        CreateLevel();
        OnAfterStep += CheckLoadLevel;
        LoadLevel(level);
        //// Draw some things on a canvas:
        //EasyDraw canvas = new EasyDraw(800, 600);
        //canvas.Clear(Color.MediumPurple);
        //canvas.Fill(Color.Yellow);
        //canvas.Ellipse(width / 2, height / 2, 200, 200);
        //canvas.Fill(50);
        //canvas.TextSize(32);
        //canvas.TextAlign(CenterMode.Center, CenterMode.Center);
        //canvas.Text("Welcome!", width / 2, height / 2);

        //// Add the canvas to the engine to display it:
        //AddChild(canvas);
        //Console.WriteLine("MyGame initialized");
    }

    private void CreateLevel()
    {
        player = new Player(50, new Vec2(250, 250));

        responsiveObject = new ResponsiveObject(30, new Vec2(10,10));
        lever = new Lever(30, new Vec2(400, 300), responsiveObject);

        AddChild(player);
        AddChild(responsiveObject);
        AddChild(lever);
    }

    // For every game object, Update is called every frame, by the engine:
    void Update()
    {
        player.Step();
        //Collisions(deltaVec,empLines,list,typeof(Player));
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
            //DestroyAll();
            AddChild(new Level(nextlevel));
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