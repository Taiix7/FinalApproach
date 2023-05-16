using GXPEngine;
using System;
using System.Reflection.Emit;
using TiledMapParser;

public class Player : AnimationSprite
{
    public int radius { get { return _radius; } }

    public bool lightWeight = true;

    public Vec2 position;

    private int _radius = 20;

    public float difline;
    public GameObject latestCollision = null;

    public float mass = .1f;

    public Vec2 velocity;

    public Vec2 acceleration;
    public Vec2 _oldPosition;

    public bool stickToWall;
    private bool moving;
    public bool ceilling;
    private bool inTheAir;

    private float tt = 0;
    Sprite sticky;

    private SoundChannel channel;
    private Sound hitGround;
    private Sound jumpSound;
    private Sound winSound;
    private bool canPlay = true;

    private bool clickedPlayer;

    public Player(TiledObject obj = null) : base("Im portant Plus.png", 8, 1)
    {
        position.x = obj.X;
        position.y = obj.Y;

        SetOrigin(_radius, _radius);

        hitGround = new Sound("slime_hit.wav");
        jumpSound = new Sound("slime_bounce.wav");
        winSound = new Sound("nextlevel.wav");
    }

    public void Gravity()
    {
        velocity += acceleration;
        position += velocity;
    }

    public void UpdateScreenPosition()
    {
        x = position.x;
        y = position.y;
    }

    void PlayerPhysics()
    {
        Gravity();
        if (!stickToWall || (stickToWall && moving) || velocity.Length() >= 0.1f)
        {
            acceleration = new Vec2(0, mass);
        }
    }

    public void Step()
    {
        if (moving && velocity.Length() > 3f)
        {
            Animate(0.03f);
        }
        if (stickToWall)
        {
            SetCycle(0, 4);
        }
        else if (!stickToWall)
        {
            SetCycle(4, 8);
        }
        PlayerControl();
        PlayerPhysics();
        CheckCollision();
        velocity *= 0.99f;
        _oldPosition = position;
        UpdateScreenPosition();
    }

    void StickyTimer()
    {

        if (stickToWall && !inTheAir)
        {
            tt += 2f / 100f;
            if (tt >= 4)
            {
                tt = 0f;
                stickToWall = false;
            }
        }
    }

    void PlayerControl()
    {
        if (ceilling)
        {
            StickyTimer();
        }

        if (Input.GetKeyDown(Key.SPACE)) stickToWall = !stickToWall;

        Vec2 deltaVec = position - new Vec2(Input.mouseX, Input.mouseY);

        if (Input.GetMouseButton(0) && IsMouseOver() && !inTheAir)
        {
            if (stickToWall)
            {
                SetFrame(1);
            }
            if (!stickToWall)
            {
                SetFrame(4);
            }
            moving = false;
            clickedPlayer = true;
        }
        else if (Input.GetMouseButtonUp(0) && clickedPlayer)
        {
            velocity += deltaVec * .05f;

            channel = jumpSound.Play();

            moving = true;
            canPlay = true;
            inTheAir = true;

            clickedPlayer = false;
        }

        if (clickedPlayer && !stickToWall)
            Gizmos.DrawArrow(position.x, position.y, deltaVec.x, deltaVec.y, 0.08f, null, 0xFF800080);
        else if (clickedPlayer && stickToWall)
            Gizmos.DrawArrow(position.x, position.y, deltaVec.x, deltaVec.y, 0.08f, null, 0xFF00FF00);

    }


    public float TOIBallLine(Player move, LineSegment line, Vec2 difference, bool top)
    {
        float t = 10;
        Vec2 lineLenght = (line.end - line.start);
        Vec2 lineNormal = lineLenght.Normal();
        Vec2 lineNormalized = lineLenght.Normalized();

        float a = difference.Dot(lineNormal) - move.radius;
        float b = -lineNormal.Dot(move.velocity);

        if (b <= 0) return 10;

        if (a >= 0)
        {
            t = a / b;
        }
        else if (a >= -move.radius)
        {
            t = 0;
        }
        else return 10;

        if (t < 1)
        {
            Vec2 POI = _oldPosition + velocity * t;
            Vec2 newDif = POI - line.start;

            float d = newDif.Dot(lineNormalized);

            if (d >= 0 && d <= lineLenght.Length()) return t;
            else return 10;

        }
        else return 10;
    }

    public void CheckCollision()
    {
        CollisionInfo firstCollision = FindEarliestCollision();
        if (firstCollision != null)
        {
            ResolveCollision(firstCollision);
        }
        else
            UpdateScreenPosition();
    }

    CollisionInfo FindEarliestCollision()
    {
        MyGame myGame = (MyGame)game;

        GameObject otherCol = null;

        Vec2 firstnormal = new Vec2(0, 1);
        float FirstTOI = 2;

        // Check other movers:
        foreach (LineSegment lines in myGame.list)
        {
            Vec2 point = lines.start;
            Vec2 line = lines.start - lines.end;

            Vec2 normalLine = line.Normal();
            Vec2 difference = point - position;

            Vec2 oldDif = _oldPosition - point;

            float ballDistance = difference.Dot(normalLine);

            if (Mathf.Abs(ballDistance) <= radius)
            {
                float t = TOIBallLine(this, lines, oldDif, false);

                if (t < FirstTOI)
                {
                    ceilling = false;
                    firstnormal = normalLine;
                    otherCol = lines;
                    FirstTOI = t;
                    difline = ballDistance;
                }
            }
        }

        foreach (LineSegment ceillings in myGame.ceillings)
        {
            Vec2 point = ceillings.start;
            Vec2 line = ceillings.start - ceillings.end;

            Vec2 normalLine = line.Normal();
            Vec2 difference = point - position;

            Vec2 oldDif = _oldPosition - point;

            float ballDistance = difference.Dot(normalLine);

            if (Mathf.Abs(ballDistance) <= radius)
            {
                float t = TOIBallLine(this, ceillings, oldDif, false);

                if (t < FirstTOI)
                {
                    ceilling = true;
                    firstnormal = normalLine;
                    otherCol = ceillings;
                    FirstTOI = t;
                    difline = ballDistance;
                }
            }
        }

        foreach (Spike spike in myGame.spikes)
        {
            Vec2 difVec = position - spike.position;

            float minDist = radius + spike.radius;

            float dist = difVec.Length();

            if (minDist + 10 > dist)
            {
                Dead();
            }
        }

        foreach (Lever lever in myGame.levers)
        {
            Vec2 difVec = position - lever.position;

            float minDist = radius + lever.radius;

            float dist = difVec.Length();

            if (minDist + 10 > dist)
            {
                if (lever.connectedObject.isActive)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        lever.NextFrame();
                    }
                }
                lever.connectedObject.isActive = false;
            }
        }

        foreach (Button button in myGame.buttons)
        {
            Vec2 difVec = position - button.position;

            float minDist = radius + button.radius;

            float dist = difVec.Length();

            if (minDist + 10 > dist)
            {
                tt += 2f / 100f;
                if (tt >= 0.4f)
                {
                    
                    switch (button.connectedObject.isActive)
                    {
                        case true:
                            button.SetFrame(6);
                            button.connectedObject.isActive = false;
                            break;
                        case false:
                            button.SetFrame(0);
                            button.connectedObject.isActive = true;
                            break;
                    }
                    tt = 0f;
                }
            }
        }

        foreach (Objective objective in myGame.objectives)
        {
            Vec2 difVec = position - objective.position;

            float minDist = radius + objective.radius;

            float dist = difVec.Length();

            if (minDist + 10 > dist)
            {
                channel = winSound.Play();
                if(myGame.level == "level_1.tmx")
                {
                    myGame.LoadLevel("level_2_real.tmx");
                    myGame.level = "level_2_real.tmx";
                }
                else if(myGame.level == "level_2_real.tmx")
                {
                    myGame.LoadLevel("level_3.tmx");
                }
            }
        }

        if (FirstTOI < 1)
        {
            return new CollisionInfo(firstnormal, otherCol, FirstTOI);
        }
        else return null;
    }

    void ResolveCollision(CollisionInfo col)
    {
        latestCollision = col.other;
        MyGame myGame = (MyGame)game;
        if (col.other is LineSegment)
        {
            inTheAir = false;

            if (canPlay)
                hitGround.Play();
            canPlay = false;

            if (stickToWall)
            {
                SetFrame(0);
                moving = false;
                acceleration = new Vec2(0, 0);
                velocity = new Vec2(0, 0);
            }
            else
            {
                SetFrame(4);
            }

            position -= (-difline + radius) * col.normal;
            velocity.Reflect(col.normal);
        }
    }

    public void ApplyForce(Vec2 force)
    {
        acceleration.x -= force.x / mass;
        acceleration.y -= force.y / mass;
    }

    public bool IsMouseOver()
    {
        float distance = Mathf.Sqrt(Mathf.Pow(Input.mouseX - position.x, 2) + Mathf.Pow(Input.mouseY - position.y, 2));
        return distance <= radius + 5;
    }

    public void Dead() {
        MyGame myGame = (MyGame)game;
        myGame.CheckLoadLevel();
        myGame.LoadLevel(myGame.currentLevel);
    }
}

