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
}

