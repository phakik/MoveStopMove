using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OS", menuName = "PantsDataSO")]

public class PantSO : ScriptableObject
{
   public List<PantItemData> pantItemDataList = new();
}
