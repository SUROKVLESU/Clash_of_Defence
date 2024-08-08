using System;
[Serializable]
public class Resources
{
    public int Gold;
    public int Iron;
    public int Power;
    public static bool operator <=(Resources resourcesL, Resources resourcesR)
    {
        if(resourcesL.Gold<=resourcesR.Gold&&resourcesL.Iron<=resourcesR.Iron&&resourcesL.Power<=resourcesR.Power)  
            return true;
        else return false;
    }
    public static bool operator >=(Resources resourcesL, Resources resourcesR)
    {
        if (resourcesL.Gold >= resourcesR.Gold && resourcesL.Iron >= resourcesR.Iron && resourcesL.Power >= resourcesR.Power)
            return true;
        else return false;
    }
    public static bool operator <(Resources resourcesL, Resources resourcesR)
    {
        if (resourcesL.Gold < resourcesR.Gold && resourcesL.Iron < resourcesR.Iron && resourcesL.Power < resourcesR.Power)
            return true;
        else return false;
    }
    public static bool operator >(Resources resourcesL, Resources resourcesR)
    {
        if (resourcesL.Gold > resourcesR.Gold && resourcesL.Iron > resourcesR.Iron && resourcesL.Power > resourcesR.Power)
            return true;
        else return false;
    }
    public static Resources operator -(Resources resourcesL, Resources resourcesR)
    {
        return new Resources() { Gold = resourcesL.Gold-resourcesR.Gold, 
            Iron = resourcesL.Iron-resourcesR.Iron, Power=resourcesL.Power-resourcesR.Power };
    }
    public static Resources operator +(Resources resourcesL, Resources resourcesR)
    {
        return new Resources()
        {
            Gold = resourcesL.Gold + resourcesR.Gold,
            Iron = resourcesL.Iron + resourcesR.Iron,
            Power = resourcesL.Power + resourcesR.Power
        };
    }
    public static Resources operator *(Resources resources, float multiplier)
    {
        return new Resources() {Gold = (int)(resources.Gold * multiplier),Iron = (int)(resources.Iron * multiplier),
            Power=(int)(resources.Power*multiplier)};
    }
}

