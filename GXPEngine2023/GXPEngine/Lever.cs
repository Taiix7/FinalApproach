using GXPEngine;
using System;
using System.Linq;
using TiledMapParser;

public class Lever : AnimationSprite
{
    public Vec2 position;
    public ResponsiveObject connectedObject;


    public int radius = 10;

    public TiledObject obj;

    public Lever(TiledObject obj = null) : base("lever.png", 2, 3)
    {
        this.obj = obj;
        position.x = obj.X;
        position.y = obj.Y;
        x = position.x;
        y = position.y;
        SetOrigin(radius, radius);
        MyGame myGame = (MyGame)game;
        myGame.levers.Add(this);

        CreateResponsiveObject();
        myGame.LateAddChild(connectedObject);
    }

    /// <summary>
    /// !!!!
    /// When you create a new lever in Tiled you need to create 2 new custom properties that are called
    /// 
    /// connectedX
    /// connectedY
    /// 
    /// And put the coordinated you want for the object that will response when the lever is detected object
    /// 
    /// NB! The names of the properties should be exactly like this!!
    /// </summary>
    void CreateResponsiveObject()
    {
        float floatValueX = 0;
        float floatValueY = 0;
        foreach (Property property in obj.propertyList.properties)
        {
            if (property.Name == "connectedX")
            {
                floatValueX = float.Parse(property.Value);
            }
            else if (property.Name == "connectedY")
            {
                floatValueY = float.Parse(property.Value);
            }
        }
        connectedObject = new ResponsiveObject(20, new Vec2(floatValueX, floatValueY));
    }

    public bool IsMouseOver()
    {
        float distance = Mathf.Sqrt(Mathf.Pow(Input.mouseX - position.x, 2) + Mathf.Pow(Input.mouseY - position.y, 2));
        return distance <= radius;
    }
}
