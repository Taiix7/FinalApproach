using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game
{

    private Player player;

    private Lever lever;
    private ResponsiveObject responsiveObject;

    public MyGame() : base(800, 600, false)     // Create a window that's 800x600 and NOT fullscreen
    {
        CreateLevel();
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
        if (lever.IsMouseOver() && Input.GetMouseButtonDown(0))
        {
            lever.connectedObject.UpdateColor(152,242,0);
        }
    }

    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}