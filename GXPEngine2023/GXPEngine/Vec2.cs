using System;
using GXPEngine; // Allows using Mathf functions

public struct Vec2
{
    public float x;
    public float y;

    public Vec2(float pX = 0, float pY = 0)
    {
        x = pX;
        y = pY;
    }

    public Vec2 MousePosition()
    {
        return new Vec2(Input.mouseX, Input.mouseY);
    }

    public float Length()
    {
        // TODO: return the vector length
        float vectorLength = (x * x) + (y * y);
        float p = Mathf.Sqrt(vectorLength);
        return p;
    }

    public void Normalize()
    {

        float vectorLength = Length();
        if (vectorLength != 0)
        {
            x = x / vectorLength;
            y = y / vectorLength;
        }
    }

    public Vec2 Normalized()
    {
        Vec2 normalized = new Vec2();
        float vectorLength = Length();

        if (vectorLength != 0)
        {
            normalized.x = x / vectorLength;
            normalized.y = y / vectorLength;
        }
        return normalized;
    }

    public void SetXY(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
    // TODO: Implement subtract, scale operators

    public static float Deg2Rad(float degrees)
    {
        return degrees * Mathf.PI / 180;
    }

    public static float Rad2Deg(float radians)
    {
        return radians * 180 / Mathf.PI;
    }

    public static Vec2 GetUnitVectorDeg(float degrees)
    {
        return GetUnitVectorRad(Deg2Rad(degrees));
    }

    public static Vec2 GetUnitVectorRad(float radians)
    {
        Vec2 vec = new Vec2();
        vec.x = 1 * Mathf.Cos(radians);
        vec.y = 1 * Mathf.Sin(radians);
        return vec;
    }

    public static Vec2 RandomUnitVector()
    {
        return GetUnitVectorDeg(Utils.Random(0, 359));
    }

    public Vec2 SetAngleDegrees(float angle)
    {
        return SetAngleRadians(Deg2Rad(angle));
    }

    public Vec2 SetAngleRadians(float angle)
    {
        float length = Length();
        this.x = Mathf.Cos(angle);
        this.y = Mathf.Sin(angle);
        this = this * length;
        return this;
    }

    public float GetAngleRadians()
    {
        float getAngle = Mathf.Atan2(y, x);
        return getAngle;
    }

    public float GetAngleDegrees()
    {
        float getAngle = Mathf.Atan2(y, x);
        float angle = Deg2Rad(getAngle);
        return angle;
    }

    public void RotateDegrees(float degrees)
    {
        RotateRadians(Deg2Rad(degrees));
    }

    public void RotateRadians(float radians)
    {
        float pX;
        float pY;
        pX = this.x * Mathf.Cos(radians) - this.y * Mathf.Sin(radians);
        pY = this.x * Mathf.Sin(radians) + this.y * Mathf.Cos(radians);
        x = pX;
        y = pY;
    }

    public Vec2 RotateAroundDegrees(Vec2 vec, float degrees)
    {
        this -= vec;

        this.RotateDegrees(degrees);

        this += vec;

        return this;
    }

    public Vec2 RotateAroundRadians(Vec2 vec, float radians)
    {
        this -= vec;

        this.RotateRadians(radians);

        this += vec;

        return this;
    }
    public float Dot(Vec2 other)
    {
        // TODO: insert dot product here
        return this.x * other.x + this.y * other.y;
    }

    public Vec2 Normal()
    {
        // TODO: return line unit normal
        return new Vec2(-y, x).Normalized();
    }

    public void Reflect(Vec2 pNormal, float pBounciness = 1)
    {
        Vec2 vec = new Vec2(x, y);
        vec = vec - (1 + pBounciness) * (vec.Dot(pNormal) * pNormal);
        SetXY(vec.x, vec.y);
    }

    public static Vec2 operator +(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x + right.x, left.y + right.y);
    }

    public static Vec2 operator +(Vec2 left, float k)
    {
        return new Vec2(left.x + k, left.y + k);
    }

    public static Vec2 operator -(Vec2 left, float k)
    {
        return new Vec2(left.x - k, left.y - k);
    }

    public static Vec2 operator -(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x - right.x, left.y - right.y);
    }

    public static Vec2 operator *(Vec2 left, float k)
    {
        return new Vec2(left.x * k, left.y * k);
    }

    public static Vec2 operator *(float left, Vec2 k)
    {
        return new Vec2(left * k.x, left * k.y);
    }

    public override string ToString()
    {
        return String.Format("({0},{1})", x, y);
    }

}