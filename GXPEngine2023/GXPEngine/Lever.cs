using GXPEngine;
using TiledMapParser;

public class Lever : AnimationSprite
{
    public Vec2 position;
    public Vent connectedObject;

    public int radius = 20;

    public TiledObject obj;

    public Lever(TiledObject obj = null) : base("button-sprite-sheet-64x64-v2.png", 6, 1)
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
    /// force
    /// 
    /// if you want to be reversed, 
    /// force = 0.5 (going down)
    /// force = -0.5 (going up)                            
    /// 
    /// And put the coordinated you want for the object that will response when the lever is detected object
    /// 
    /// NB! The names of the properties should be exactly like this!!
    /// </summary>
    void CreateResponsiveObject()
    {
        float floatValueX = 0;
        float floatValueY = 0;
        float force = 0.5f;

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
            else if (property.Name == "force")
            {
                //force = float.Parse(property.Value);
            }
        }
        connectedObject = new Vent(new Vec2(floatValueX, floatValueY), force);
        if (force == 0.5f)
            connectedObject.SetScaleXY(connectedObject.scaleX, connectedObject.scaleY * -1);
        else
        {
            connectedObject.SetScaleXY(connectedObject.scaleX, connectedObject.scaleY);
        }
    }
}
