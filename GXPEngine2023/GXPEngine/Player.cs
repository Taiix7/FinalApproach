using GXPEngine;
using System;
using TiledMapParser;

class Player : Sprite
{
    public int radius { get { return _radius; } }

    public bool lightWeight = true;

    public Vec2 position;

    private int _radius = 20;

    public float difline;
    public GameObject latestCollision = null;

    private float speed = 1f;
    private float weight = .1f;
    private float jumpSpeed = 5;

    public Vec2 velocity;
    private Vec2 acceleration;
    private Vec2 _oldPosition;

    private bool canJump;
    private bool stickToWall;

    public Player(TiledObject obj = null) : base("Slime_Luca.png")
    {
        position.x = obj.X;
        position.y = obj.Y;
        //UpdateScreenPosition();
        SetOrigin(_radius, _radius);
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
        acceleration = new Vec2(0, weight);
    }

    public void Step()
    {
        PlayerPhysics();
        PlayerControl();
        CheckCollision();

        velocity *= 0.99f;
        _oldPosition = position;
        UpdateScreenPosition();
    }

    void PlayerControl()
    {
        //if (Input.GetKey(Key.A)) { position -= new Vec2(speed * Time.deltaTime, 0); }
        //if (Input.GetKey(Key.D)) { position += new Vec2(speed * Time.deltaTime, 0); }

        //if (!lightWeight) return;
        //if (canJump && Input.GetKeyDown(Key.SPACE))
        //{
        //    velocity -= new Vec2(0, jumpSpeed);
        //    canJump = false;
        //}
        Vec2 deltaVec = position - new Vec2(Input.mouseX, Input.mouseY);
        if (velocity.Length() < 0.1f)
        {
            if (Input.GetMouseButton(0))
            {
                Gizmos.DrawArrow(position.x, position.y, deltaVec.x, deltaVec.y, 0.08f);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                velocity += deltaVec * .04f;
            }
        }
        if(Input.GetKey(Key.SPACE)) { stickToWall = !stickToWall; }
    }

    public void Land() { canJump = true; }

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
                    firstnormal = normalLine;
                    otherCol = lines;
                    FirstTOI = t;
                    difline = ballDistance;

                }
            }
        }

        foreach(Spike spike in myGame.spikes)
        {
            Vec2 difVec = position - spike.position;

            float minDist = radius + spike.radius;

            float dist = difVec.Length();

            if (minDist + 10 > dist)
            {
                position.x = 150;
                position.y = 700;
                velocity = new Vec2(0,0);
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
            if (stickToWall)
            {
                acceleration = new Vec2(0,0);
                velocity = new Vec2(0, 0);
            }
            position -= (-difline + radius) * col.normal;
            velocity.Reflect(col.normal);
        }
    }
}
