using System;
using GXPEngine;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

public class Player : EasyDraw
{
    public int radius
    {
        get
        {
            return _radius;
        }
    }

    public Vec2 velocity;
    public Vec2 position;
    public Vec2 oldPosition;
    public Vec2 acceleration = new Vec2(0, 0f);

    public GameObject latestCollision = null;
    GameObject picture;

    //Difference of line/ball need for resolve line
    public float difline;

    Vec2 _oldPosition;

    public Grape grape;
    public Candy candy;
    public Tomato tomato;

    int _radius;
    float _speed;
    bool stickyBall;

    public int movesLeft = 7;

    public List<Collectables> _balls;


    public Player(int pRadius, Vec2 pPosition, float pSpeed = 5) : base(pRadius * 2 + 1, pRadius * 2 + 1)
    {
        _radius = pRadius;
        position = pPosition;
        _speed = pSpeed;
        UpdateScreenPosition();
        SetOrigin(_radius, _radius);
        Draw(255, 255, 255);

        grape = new Grape();
        candy = new Candy();
        tomato = new Tomato();

    }

    void Draw(byte red, byte green, byte blue)
    {
        //Fill(red, green, blue);
        //Stroke(red, green, blue);
        //Ellipse(_radius, _radius, 2 * _radius, 2 * _radius);
    }

    public void FollowMouse()
    {
        velocity += acceleration;
        position += velocity;
    }

    public void UpdateScreenPosition()
    {
        x = position.x;
        y = position.y;

    }

    void BallControl()
    {
        _balls = new List<Collectables>();
        Vec2 deltaVec = position - new Vec2(Input.mouseX, Input.mouseY);
        if (movesLeft > 0 && velocity.Length() < 0.1f)
        {
            if (Input.GetMouseButton(0))
            {
                Gizmos.DrawArrow(position.x, position.y, deltaVec.x, deltaVec.y,0.08f,picture);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                velocity += deltaVec * .01f;
                acceleration = new Vec2(0, 0f);
                movesLeft--;
            }
        }
        if (Input.GetKeyDown(Key.Q))
        {
            movesLeft++;
        }
    }

    public void Step()
    {
        FollowMouse();
        BallControl();

        velocity *= 0.99f;
        _oldPosition = position;

        CheckCollision();
        StickyBall();
        UpdateScreenPosition();
        Animations();
        CheckAnimation();
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
        foreach (LineSegment lines in myGame._lines)
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
        foreach (LineSegment lines in myGame._breakableLines)
        {

            Vec2 point = lines.start;
            Vec2 line = lines.start - lines.end;

            Vec2 normalLine = line.Normal();
            Vec2 difference = point - position;

            Vec2 oldDif = _oldPosition - point;

            float ballDistance = difference.Dot(normalLine);

            if (Mathf.Abs(ballDistance) < radius)
            {

                float t = TOIBallLine(this, lines, oldDif, false);
                if (t < FirstTOI)
                {
                    if(velocity.Length() > 1)
                    {
                        lines.Remove();
                        lines.start = new Vec2(0, 0);
                        lines.end = new Vec2(0, 0);
                        lines.Destroy();
                    }
                    firstnormal = normalLine;
                    otherCol = lines;
                    FirstTOI = t;
                    difline = ballDistance;
                }

                if (((MyGame)game).collectables > 5)
                {
                    lines.Remove();
                    lines.start = new Vec2(0,0);
                    lines.end = new Vec2(0,0);
                    lines.Destroy();
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
            if (velocity.Length() > 5f)
            {
                myGame.Reset();
            }
            if (stickyBall)
            {
                velocity = new Vec2(0, 0);
            }
            position -= (-difline + radius) * col.normal;
            velocity.Reflect(col.normal);
        }
    }

    void StickyBall()
    {
        MyGame myGame = (MyGame)game;
        if (myGame.levelIndex == 4)
        {
            stickyBall = true;
        }
    }

    void Animations()
    {
        Vec2 deltaVec = new Vec2(Input.mouseX, Input.mouseY) - position;

        if (velocity.Length() > .1f)
        {
            grape.Animate(velocity.Length() / 10);
            candy.Animate(velocity.Length() / 10);
            grape.Animate(velocity.Length() / 10);
            tomato.Animate(velocity.Length() / 5);
        }
        grape.rotation = (deltaVec.GetAngleRadians() * 180 / Mathf.PI);
        candy.rotation = (deltaVec.GetAngleRadians() * 180 / Mathf.PI) - 90;
        grape.rotation = (deltaVec.GetAngleRadians() * 180 / Mathf.PI) - 90;
        tomato.rotation = (deltaVec.GetAngleRadians() * 180 / Mathf.PI) - 90;
    }

    void CheckAnimation()
    {
        MyGame myGame = (MyGame)game;
        switch (myGame.collectables)
        {
            case 0:
                candy.SetCycle(1, 7);
                break;
            case 1:
                candy.SetCycle(7, 7);
                break;
            case 2:
                candy.SetCycle(14, 8);
                break;
            case 3:
                candy.SetCycle(22, 11);
                break;
            case 4:
                candy.SetCycle(33, 10);
                break;
            case 5:
                candy.SetCycle(43, 15);
                break;
        }
    }
}
