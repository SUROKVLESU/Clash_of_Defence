using System;
[Serializable]
public class SizeMapCell
{
    public Line[] Sizes;
    [Serializable]
    public class Line
    {
        public bool[] line;
    }
}

