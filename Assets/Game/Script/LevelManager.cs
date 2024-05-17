using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{

    GameObject map;
    GameObject mapPrefab;

    public void LoadMap(string index)
    {
        map = Resources.Load<GameObject>($"{CONSTANT.MAP_PATH}{index}");
        mapPrefab = ObjectPooling.Instance.GetObject(map, Vector3.zero);
        mapPrefab.transform.rotation = Quaternion.Euler(-90,0,0);
        mapPrefab.GetComponent<Level>().OnInit();
        mapPrefab.SetActive(true);
    }
    public void DeleteMap()
    {
        if (mapPrefab != null)
        {
            mapPrefab.GetComponent<Level>().OnDesPawn();
        }
    }
    public void CheckDeadEnemyOnMap(Enemy enemy)
    {
        if (mapPrefab != null)
        {
            mapPrefab.GetComponent<Level>().CheckDeadEnemy(enemy);
        }
    }



}
