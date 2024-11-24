using System.Collections.Generic;

public class GameStatusSavedData:LocalSaveData
{
    public int UnlockLevelCount; 
    public int CurSelectedLevel;

    //·ûÎÄÊÇ·ñ½âËø
    public Dictionary<int,bool> RunesRecord=new();

}
