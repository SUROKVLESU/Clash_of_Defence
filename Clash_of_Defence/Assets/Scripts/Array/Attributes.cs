﻿using System;
[Serializable]
public class Attributes
{
    public float Physical;
    public float Acid;
    public float Ice;
    public static Attributes operator -(Attributes attributes1, Attributes attributes2)
    {
        Attributes attributes = new Attributes() { Physical= attributes1.Physical - attributes2.Physical ,
        Acid = attributes1.Acid - attributes2.Acid,Ice = attributes1.Ice - attributes2.Ice};
        if (attributes.Physical <= 0)  attributes.Physical = 0.1f;
        if (attributes.Acid <= 0) attributes.Acid = 0.1f;
        if (attributes.Ice <= 0) attributes.Ice = 0.1f;
        return attributes;
    }
    public static Attributes operator +(Attributes attributes1, Attributes attributes2)
    {
        Attributes attributes = new Attributes()
        {
            Physical = attributes1.Physical + attributes2.Physical,
            Acid = attributes1.Acid + attributes2.Acid,
            Ice = attributes1.Ice + attributes2.Ice
        };
        return attributes;
    }
    public static float operator -(float hp, Attributes attributes)
    {
        return hp-attributes.Physical-attributes.Acid-attributes.Ice;
    }
    public static bool operator >(Attributes attributes, int value)
    {
        if (attributes.Physical>0||attributes.Ice>0||attributes.Acid>0)
        {
            return true;
        }
        else return false;
    }
    public static bool operator <(Attributes attributes, int value)
    {
        return !(attributes>value);
    }
    public static Attributes operator /(Attributes attributes, int value)
    {
        return new Attributes()
        {
            Physical = attributes.Physical / value,
            Ice = attributes.Ice / value,
            Acid = attributes.Acid / value
        };
    }
}

