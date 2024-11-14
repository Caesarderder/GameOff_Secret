using System;
using System.Collections.Generic;

[Serializable]
public class TableLevel
{
    public int Id;
    public List<int> RuneIdsA=new();
    public List<int> RuneIdsB=new();
}

[Serializable]
public class TableItem
{
    public int Id;
    public EItemType Type;
}

public enum EItemType
{
    None=0,
    Rune=1,
    Normal=2,
    NeedItem=3,
}

[Serializable]
public class TableSprite
{
    public int Id;
}

