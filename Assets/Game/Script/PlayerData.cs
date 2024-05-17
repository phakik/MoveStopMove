using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public string name;
    public int level;
    public Enum.WeaponType currentWeapon;
    public Enum.PantType pantType;
    public List<int> pantsData;
    public List<int> weaponData;
    public PlayerData() { }
    public PlayerData(string name, int level, Enum.WeaponType weapon, List<int> weaponData, Enum.PantType pantType, List<int> pantsData)
    {
        this.name = name;
        this.level = level;
        this.currentWeapon = weapon;
        this.weaponData = weaponData;
        this.pantType = pantType;
        this.pantsData = pantsData;
    }
}
