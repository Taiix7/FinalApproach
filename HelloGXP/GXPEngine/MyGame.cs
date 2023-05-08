using System;
using GXPEngine;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

public class MyGame : Game
{
    public List<LineSegment> _lines;
    public List<LineSegment> _breakableLines;

    public List<Collectables> _balls;

    public Vec2 deltaVec = new Vec2();
    public Vec2 line;

    public int levelIndex = 0;

    public int collectables;

    Player player;
    HUD hud;

    Sprite branch;
    Sprite branch2;
    Sprite branch3;
   
    //Main menu
    Sprite background;
    Button button;

    SoundChannel soundChannel;
    Sound backgroundSound;

    SoundChannel moveSoundChannel;
    Sound tomatoMove;

    SoundChannel tomatoDead;
    Sound tomatoDeadSound;

    int lasting = 50;
    public MyGame() : base(1366, 768, false)
    {

        _lines = new List<LineSegment>();
        _breakableLines = new List<LineSegment>();

        _balls = new List<Collectables>();

        collectables = 0;
        CreateLevel(levelIndex);

        backgroundSound = new Sound("Sounds/In_Game_Music.mp3", true);
        soundChannel = backgroundSound.Play();
        soundChannel.Volume = 0.3f;

        tomatoMove = new Sound("Sounds/Tomato_Move.wav");
        moveSoundChannel = tomatoMove.Play();
        moveSoundChannel.Stop();

        tomatoDeadSound = new Sound("Sounds/Tomato_Dead.wav");
        tomatoDead = tomatoDeadSound.Play();
        tomatoDead.Stop();
    }

    void CreateLevel(int index)
    {
        levelIndex = index;

        foreach (LineSegment lines in _lines)
        {
            lines.Destroy();
        }
        foreach (LineSegment lines in _breakableLines)
        {
            lines.Destroy();
        }

        foreach (Collectables balls in _balls)
        {
            balls.Destroy();
        }

        if (player != null && hud != null)
        {
            player.Destroy();
            hud.Destroy();
        }

        _lines.Clear();
        _breakableLines.Clear();
        _balls.Clear();



        switch (index)
        {
            case 0:
                MainMenuLevel();
                break;
            case 1:
                background.Destroy();
                button.Destroy();
                FirstLevel();

                break;
            case 2:
                SecondLevel();
                break;
            case 3:
                ThirdLevel();
                break;
            case 4:
                FourLevel();
                break;
        }

        foreach (LineSegment lines in _lines)
        {
            AddChild(lines);
        }

        foreach (LineSegment lines in _breakableLines)
        {
            AddChild(lines);
        }

        foreach (Collectables balls in _balls)
        {
            AddChild(balls);
        }
    }

    void MainMenuLevel()
    {

        background = new Sprite("background.png");
        AddChild(background);
        button = new Button("button.png");
        AddChild(button);
        button.SetXY(width / 2, height / 2);
    }

    void MainMenu()
    {
        if (Input.mouseX < button.x + width / 2 && Input.mouseX > button.x - width / 2 &&
               Input.mouseY < button.y + button.height / 2 && Input.mouseY > button.y - button.height / 2 && Input.GetMouseButtonDown(0))
        {
            levelIndex++;
            CreateLevel(levelIndex);
        }
    }

    void FirstLevel()
    {
        if (levelIndex == 1)
        {
            background = new Sprite("level_1_bg.png", false, false);
            AddChild(background);
        }

        player = new Player(10, new Vec2(250, 250));
        AddChild(player);
        player.AddChild(player.grape);
        hud = new HUD(player);
        AddChild(hud);

        //Left
        _lines.Add(new LineSegment(new Vec2(200, 300), new Vec2(200, 200), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(400, 500), new Vec2(400, 298), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(400, 200), new Vec2(400, 100), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(600, 600), new Vec2(600, 500), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(900, 700), new Vec2(900, 600), 0xffffffff, 3));

        //Right
        _lines.Add(new LineSegment(new Vec2(1300, 100), new Vec2(1300, 700), 0xffffffff, 3));

        //Bottom
        _lines.Add(new LineSegment(new Vec2(400, 300), new Vec2(200, 300), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(600, 500), new Vec2(400, 500), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(900, 600), new Vec2(600, 600), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1300, 700), new Vec2(900, 700), 0xffffffff, 3));

        //Top
        _lines.Add(new LineSegment(new Vec2(198, 200), new Vec2(400, 200), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(398, 100), new Vec2(1300, 100), 0xffffffff, 3));

        //Obj_1
        _lines.Add(new LineSegment(new Vec2(1200, 200), new Vec2(498, 200), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(600, 400), new Vec2(600, 350), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(600, 350), new Vec2(1200, 350), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1200, 350), new Vec2(1200, 200), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(498, 400), new Vec2(600, 400), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(500, 200), new Vec2(500, 400), 0xffffffff, 3));

        //Obj_2
        _lines.Add(new LineSegment(new Vec2(1200, 450), new Vec2(700, 450), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1200, 600), new Vec2(1200, 450), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1000, 600), new Vec2(1200, 600), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1000, 500), new Vec2(1000, 600), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(700, 500), new Vec2(1000, 500), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(700, 450), new Vec2(700, 500), 0xffffffff, 3));




        //Obstacles
        //!!!!!!!!!! PUT THE OBSTACLES HERE !!!!!!!!!!!1
        _balls.Add(new Collectables("BlackHole", new Vec2(650, 110), 38, 255, 255, 0));
        _balls.Add(new Collectables("Goal", new Vec2(1200, 300), 50, 255, 255, 0));
    }

    void SecondLevel()
    {
        if (levelIndex == 2)
        {
            background = new Sprite("level_2_bg.png", false, false);
            AddChild(background);
        }

        player = new Player(10, new Vec2(200, 525));
        AddChild(player);
        player.AddChild(player.grape);
        hud = new HUD(player);
        AddChild(hud);


        //Start
        _lines.Add(new LineSegment(new Vec2(150, 550), new Vec2(150, 500), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(250, 550), new Vec2(150, 550), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(148, 500), new Vec2(250, 500), 0xffffffff, 3));

        //Left
        _lines.Add(new LineSegment(new Vec2(250, 600), new Vec2(250, 550), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(250, 600), new Vec2(250, 550), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(400, 700), new Vec2(400, 600), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(250, 500), new Vec2(250, 450), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(350, 450), new Vec2(350, 100), 0xffffffff, 3));

        //Right
        _lines.Add(new LineSegment(new Vec2(1300, 100), new Vec2(1300, 700), 0xffffffff, 3));

        //Bottom
        _lines.Add(new LineSegment(new Vec2(400, 600), new Vec2(250, 600), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1300, 700), new Vec2(400, 700), 0xffffffff, 3));

        //Top
        _lines.Add(new LineSegment(new Vec2(250, 450), new Vec2(350, 450), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(350, 100), new Vec2(1300, 100), 0xffffffff, 3));

        //Obj_TOP
        _lines.Add(new LineSegment(new Vec2(1250, 150), new Vec2(400, 150), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1250, 350), new Vec2(1250, 150), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1000, 350), new Vec2(1250, 350), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1000, 250), new Vec2(1000, 350), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(400, 250), new Vec2(1000, 250), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(400, 150), new Vec2(400, 250), 0xffffffff, 3));

        //Obj_START_TOP
        _lines.Add(new LineSegment(new Vec2(650, 500), new Vec2(400, 500), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(650, 500), new Vec2(650, 400), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(650, 400), new Vec2(400, 400), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(400, 400), new Vec2(400, 500), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(400, 500), new Vec2(650, 500), 0xffffffff, 3));

        //Obj_START_TOP_RECT
        _lines.Add(new LineSegment(new Vec2(950, 400), new Vec2(700, 400), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(950, 500), new Vec2(950, 400), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(700, 500), new Vec2(950, 500), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(700, 400), new Vec2(700, 500), 0xffffffff, 3));

        //Obj_START_BOTTOM
        _lines.Add(new LineSegment(new Vec2(650, 550), new Vec2(450, 550), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(650, 650), new Vec2(650, 550), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(450, 650), new Vec2(650, 650), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(450, 550), new Vec2(450, 650), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(450, 550), new Vec2(650, 550), 0xffffffff, 3));

        //Obj_START_BOTTOM_OBJ
        _lines.Add(new LineSegment(new Vec2(700, 650), new Vec2(1250, 650), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1250, 650), new Vec2(1250, 400), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1250, 400), new Vec2(1000, 400), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1000, 400), new Vec2(1000, 550), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1000, 550), new Vec2(700, 550), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(700, 550), new Vec2(700, 650), 0xffffffff, 3));

        //Obj_MID_RECT_LEFT
        _lines.Add(new LineSegment(new Vec2(950, 300), new Vec2(400, 300), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(950, 350), new Vec2(950, 300), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(400, 350), new Vec2(950, 350), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(400, 300), new Vec2(400, 350), 0xffffffff, 3));

        //OBSTACLES
        _balls.Add(new Collectables("BlackHole", new Vec2(925, 100), 25, 255, 255, 0));
        _balls.Add(new Collectables("BlackHole", new Vec2(955, 425), 25, 255, 255, 0));
        _balls.Add(new Collectables("BlackHole", new Vec2(775, 650), 25, 255, 255, 0));
        _balls.Add(new Collectables("Goal", new Vec2(1240, 105), 30, 255, 255, 0));
    }

    void ThirdLevel()
    {

        if (levelIndex == 3)
        {
            background = new Sprite("level_3_bg.png", false, false);
            AddChild(background);
        }

        player = new Player(10, new Vec2(750, 125));
        AddChild(player);
        player.AddChild(player.tomato);
        hud = new HUD(player);
        AddChild(hud);

        //Left
        _lines.Add(new LineSegment(new Vec2(350, 700), new Vec2(350, 550), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(350, 450), new Vec2(350, 100), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(550, 450), new Vec2(550, 550), 0xffffffff, 3));

        //Right
        _lines.Add(new LineSegment(new Vec2(1300, 100), new Vec2(1300, 700), 0xffffffff, 3));

        //Bottom
        _lines.Add(new LineSegment(new Vec2(550, 450), new Vec2(350, 450), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1300, 700), new Vec2(350, 700), 0xffffffff, 3));

        //Top
        _lines.Add(new LineSegment(new Vec2(350, 550), new Vec2(550, 550), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(350, 100), new Vec2(1300, 100), 0xffffffff, 3));

        //Obj_MID_RECT_LEFT
        _lines.Add(new LineSegment(new Vec2(550, 600), new Vec2(400, 600), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(550, 650), new Vec2(550, 600), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(400, 650), new Vec2(550, 650), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(400, 600), new Vec2(400, 650), 0xffffffff, 3));

        //Obj_MID_RECT_LEFT
        _lines.Add(new LineSegment(new Vec2(500, 150), new Vec2(400, 150), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(400, 150), new Vec2(400, 400), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(400, 400), new Vec2(550, 400), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(550, 400), new Vec2(550, 350), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(550, 350), new Vec2(500, 350), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(500, 350), new Vec2(500, 150), 0xffffffff, 3));

        //Obj_MID_RECT_LEFT
        _lines.Add(new LineSegment(new Vec2(1250, 150), new Vec2(550, 150), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1250, 300), new Vec2(1250, 150), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1050, 300), new Vec2(1250, 300), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1050, 400), new Vec2(1050, 300), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(850, 400), new Vec2(1050, 400), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(850, 300), new Vec2(850, 400), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(550, 300), new Vec2(850, 300), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(550, 150), new Vec2(550, 300), 0xffffffff, 3));

        //OBJ
        _lines.Add(new LineSegment(new Vec2(800, 350), new Vec2(600, 350), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(800, 400), new Vec2(800, 350), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(600, 400), new Vec2(800, 400), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(600, 350), new Vec2(600, 400), 0xffffffff, 3));

        //OBJ
        _lines.Add(new LineSegment(new Vec2(1050, 450), new Vec2(600, 450), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1050, 550), new Vec2(1050, 450), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(600, 550), new Vec2(1050, 550), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(600, 450), new Vec2(600, 550), 0xffffffff, 3));

        //OBJ
        _lines.Add(new LineSegment(new Vec2(900, 600), new Vec2(600, 600), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(900, 650), new Vec2(900, 600), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(600, 650), new Vec2(900, 650), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(600, 600), new Vec2(600, 650), 0xffffffff, 3));

        //Obj_START_BOTTOM_OBJ
        _lines.Add(new LineSegment(new Vec2(950, 650), new Vec2(1250, 650), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1250, 650), new Vec2(1250, 350), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1250, 350), new Vec2(1100, 350), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1100, 350), new Vec2(1100, 600), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1100, 600), new Vec2(950, 600), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(950, 600), new Vec2(950, 650), 0xffffffff, 3));

        //OBSTACLES
        _breakableLines.Add(new LineSegment(new Vec2(950, 100), new Vec2(950, 150), 0xffffffff, 3));
        _breakableLines.Add(new LineSegment(new Vec2(600, 375), new Vec2(550, 375), 0xffffffff, 3));
        _breakableLines.Add(new LineSegment(new Vec2(600, 625), new Vec2(550, 625), 0xffffffff, 3));
        _breakableLines.Add(new LineSegment(new Vec2(950, 625), new Vec2(900, 625), 0xffffffff, 3));
        branch = new Sprite("branch.png");
        branch.SetOrigin(branch.width/2,branch.height/2);
        branch.SetXY(575, 375);
        branch.SetScaleXY(0.08f);
        AddChild(branch);


        branch2 = new Sprite("branch.png");
        branch2.SetOrigin(branch.width / 2, branch.height / 2);
        branch2.SetXY(540, 620);
        branch2.SetScaleXY(0.08f);
        AddChild(branch2);

        branch3 = new Sprite("branch.png");
        branch3.SetOrigin(branch.width / 2, branch.height / 2);
        branch3.SetXY(890, 620);
        branch3.SetScaleXY(0.08f);
        AddChild(branch3);

        branch3 = new Sprite("branch.png");
        branch3.SetOrigin(branch.width / 2, branch.height / 2);
        branch3.SetXY(955, 100);
        branch3.SetScaleXY(0.08f);
        branch3.rotation = 90;
        AddChild(branch3);

        _balls.Add(new Collectables("BlackHole", new Vec2(805, 400), 25, 255, 255, 0));
        _balls.Add(new Collectables("BlackHole", new Vec2(500, 650), 25, 255, 255, 0));
        _balls.Add(new Collectables("Goal", new Vec2(725, 640), 30, 255, 255, 0));

    }

    void FourLevel()
    {
        if (levelIndex == 4)
        {
            background = new Sprite("level_4_bg.png", false, false);
            AddChild(background);
        }

        player = new Player(5, new Vec2(1150, 125));
        AddChild(player);
        player.AddChild(player.candy);
        player.candy.SetCycle(1, 7);
        hud = new HUD(player);
        AddChild(hud);

        hud = new HUD(player);
        AddChild(hud);

        //Left
        _lines.Add(new LineSegment(new Vec2(350, 700), new Vec2(350, 100), 0xffffffff, 3));

        //Right
        _lines.Add(new LineSegment(new Vec2(1300, 100), new Vec2(1300, 700), 0xffffffff, 3));

        //Bottom
        _lines.Add(new LineSegment(new Vec2(1300, 700), new Vec2(350, 700), 0xffffffff, 3));

        //Top
        _lines.Add(new LineSegment(new Vec2(350, 100), new Vec2(1300, 100), 0xffffffff, 3));

        //OBJ
        _lines.Add(new LineSegment(new Vec2(550, 450), new Vec2(400, 450), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(550, 650), new Vec2(550, 450), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(400, 650), new Vec2(550, 650), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(400, 450), new Vec2(400, 650), 0xffffffff, 3));

        //Obj_1
        _lines.Add(new LineSegment(new Vec2(700, 150), new Vec2(400, 150), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(700, 250), new Vec2(700, 150), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(550, 250), new Vec2(700, 250), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(550, 400), new Vec2(550, 250), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(550, 400), new Vec2(400, 400), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(400, 150), new Vec2(400, 400), 0xffffffff, 3));

        //Obj_1
        _lines.Add(new LineSegment(new Vec2(1250, 150), new Vec2(750, 150), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1250, 450), new Vec2(1250, 150), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1200, 450), new Vec2(1250, 450), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1200, 250), new Vec2(1200, 450), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1200, 250), new Vec2(750, 250), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(750, 150), new Vec2(750, 250), 0xffffffff, 3));

        //OBJ
        _lines.Add(new LineSegment(new Vec2(1150, 300), new Vec2(1000, 300), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1150, 450), new Vec2(1150, 300), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1000, 450), new Vec2(1150, 450), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1000, 300), new Vec2(1000, 450), 0xffffffff, 3));


        //Obj_MID_RECT_LEFT
        _lines.Add(new LineSegment(new Vec2(950, 300), new Vec2(600, 300), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(950, 300), new Vec2(950, 500), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(950, 500), new Vec2(1150, 500), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1150, 650), new Vec2(1150, 500), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1150, 650), new Vec2(600, 650), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(600, 650), new Vec2(600, 300), 0xffffffff, 3));

        //OBJ
        _lines.Add(new LineSegment(new Vec2(1250, 500), new Vec2(1200, 500), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1250, 650), new Vec2(1250, 500), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1200, 650), new Vec2(1250, 650), 0xffffffff, 3));
        _lines.Add(new LineSegment(new Vec2(1200, 500), new Vec2(1200, 650), 0xffffffff, 3));

        //OBSTACLES
        _breakableLines.Add(new LineSegment(new Vec2(750, 225), new Vec2(700, 225), 0xffffffff, 3));
        _breakableLines.Add(new LineSegment(new Vec2(1300, 225), new Vec2(1250, 225), 0xffffffff, 3));

        branch3 = new Sprite("branch.png");
        branch3.SetOrigin(branch.width / 2, branch.height / 2);
        branch3.SetXY(685, 220);
        branch3.SetScaleXY(0.08f);
        AddChild(branch3);

        branch3 = new Sprite("branch.png");
        branch3.SetOrigin(branch.width / 2, branch.height / 2);
        branch3.SetXY(1245, 220);
        branch3.SetScaleXY(0.08f);
        AddChild(branch3);

        _balls.Add(new Collectables("BlackHole", new Vec2(1155, 575), 25, 255, 255, 0));
        _balls.Add(new Collectables("PickUp", new Vec2(1050, 110), 15, 255, 255, 0));
        _balls.Add(new Collectables("PickUp", new Vec2(950, 110), 15, 255, 255, 0));
        _balls.Add(new Collectables("PickUp", new Vec2(850, 110), 15, 255, 255, 0));
        _balls.Add(new Collectables("PickUp", new Vec2(450, 110), 15, 255, 255, 0));
        _balls.Add(new Collectables("PickUp", new Vec2(360, 110), 15, 255, 255, 0));
        _balls.Add(new Collectables("PickUp", new Vec2(360, 210), 15, 255, 255, 0));
        _balls.Add(new Collectables("PickUp", new Vec2(360, 450), 15, 255, 255, 0));
        _balls.Add(new Collectables("PickUp", new Vec2(360, 560), 15, 255, 255, 0));
        _balls.Add(new Collectables("PickUp", new Vec2(460, 660), 15, 255, 255, 0));
        _balls.Add(new Collectables("PickUp", new Vec2(680, 660), 15, 255, 255, 0));
        _balls.Add(new Collectables("Goal", new Vec2(1000, 640), 30, 255, 255, 0));
    }

    void Update()
    {
        Console.WriteLine(lasting);
        if (Input.GetKeyDown(Key.SPACE))
        {
            levelIndex++;
            CreateLevel(levelIndex);
        }
        if (player != null && hud != null)
        {
            player.Step();
            hud.Check();
        }
        Death();
        PickUp();
        if (levelIndex == 0)
        {
            MainMenu();
        }
        Sounds();
    }

    public void Death()
    {

        foreach (Collectables balls in _balls.ToList())
        {
            if (balls.Name() == "BlackHole")
            {
                Vec2 difVec = player.position - balls.position;

                float minDist = player.radius + balls.radius;

                float dist = difVec.Length();

                if (minDist + 10 > dist)
                {
                    if (levelIndex == 3)
                    {
                        tomatoDead = tomatoDeadSound.Play();
                        
                    }
                    Reset();
                }
                else { balls.SetColor(1, 1, 1); }
            }
        }

        if (player != null && player.movesLeft <= 0)
        {
            Reset();
        }

        if (collectables >= 10)
        {
            Reset();
        }

    }

    void Sounds()
    {
        if (levelIndex == 3)
        {
            if (player.velocity.Length() > 0.2f && moveSoundChannel.IsPlaying == false)
            {
                moveSoundChannel = tomatoMove.Play();
            }
        }
    }

    public void PickUp()
    {

        foreach (Collectables balls in _balls.ToList())
        {
            if (balls.Name() == "PickUp")
            {
                Vec2 difVec = player.position - balls.position;

                float minDist = player.radius + balls.radius;

                float dist = difVec.Length();

                if (minDist > dist)
                {
                    collectables++;
                    balls.radius = -10;
                    balls.Destroy();
                    balls.Remove();

                }
            }

            if (balls.Name() == "Goal")
            {
                Vec2 difVec = player.position - balls.position;

                float minDist = player.radius + balls.radius;

                float dist = difVec.Length();

                if (minDist + 10 > dist)
                {
                    levelIndex++;
                    CreateLevel(levelIndex);
                }
            }
        }
    }

    public void Reset()
    {
        if (levelIndex == 3)
        {
            tomatoDead.Stop();
        }
        CreateLevel(levelIndex);
        if (levelIndex == 4)
        {
            collectables=0;
        }
    }

    static void Main()
    {
        new MyGame().Start();
    }
}
