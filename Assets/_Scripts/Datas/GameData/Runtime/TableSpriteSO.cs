using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TableSpriteSO", menuName = "Scriptable Objects/TableSpriteSO")]
public class TableSpriteSO : ScriptableObject
{
    public List<TableSprite> TableLevel=new();
}

