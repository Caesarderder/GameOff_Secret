using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TableItemSO", menuName = "Scriptable Objects/TableItemSO")]
public class TableItemSO : ScriptableObject
{
    public List<TableItem> TableLevel=new();


}
