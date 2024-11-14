using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TableLevelSO", menuName = "Scriptable Objects/TableLevelSO")]
public class TableLevelSO: ScriptableObject
{
    public List<TableLevel> TableLevel=new();
}

