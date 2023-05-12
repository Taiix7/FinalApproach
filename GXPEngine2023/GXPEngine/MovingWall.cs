using GXPEngine;
using System;
using TiledMapParser;


public class MovingWall : Sprite
{
    private float speed = 0.15f;
    public int radius = 10;
    public Vec2 position;
    public bool moveDown = true;
    public MovingWall(TiledObject obj = null) : base("circle.png")
    {
        position.x = obj.X;
        position.y = obj.Y;
        x = position.x;
        y = position.y;
        SetOrigin(radius, radius);
    }

    public void Update()
    {
        if(y < 800 && y > 450 && moveDown)
        {
            y += speed*Time.deltaTime;
        }
        else if(y < 800 && y > 450 && !moveDown)
        {
            y -= speed*Time.deltaTime;
        }
        if (y > 800)
        {
            moveDown = false;
            y -= speed * Time.deltaTime;
        }
        if(y < 450)
        {
            moveDown = true;
            y += speed * Time.deltaTime;
        }
        //if(position.y > 800)
        //{
        //    position.y -= speed;
        //}
        //if(position.y < 200)
        //{
        //    position.y += speed;
        //}
    }
}