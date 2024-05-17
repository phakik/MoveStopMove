using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OS", menuName = "WeaponDataSO")]
public class WeaponSO : ScriptableObject
{
     public List<WeaponItemData> weaponDataList = new();
}
