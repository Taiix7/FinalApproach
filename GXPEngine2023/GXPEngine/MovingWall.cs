using GXPEngine;
using TiledMapParser;


public class MovingWall : Sprite
{
    private float speed = 0.15f;
    public int radius = 10;
    public Vec2 position;
    public bool moveDown = true;
    NLineSegment moveLine;
    public MovingWall(TiledObject obj = null) : base("Empty.png")
    {
        position.x = obj.X;
        position.y = obj.Y;
        x = position.x;
        y = position.y;
        SetOrigin(radius, radius);
        moveLine = new NLineSegment(obj.X + obj.Width, obj.Y, obj.X + obj.Width, obj.Y + obj.Height, 0xffffffff, 3);
        MyGame myGame = (MyGame)game;
        myGame.list.Add(moveLine);
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

        moveLine.x = position.x;
        moveLine.y = position.y;
    }
}