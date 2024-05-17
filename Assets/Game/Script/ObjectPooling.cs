using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private static ObjectPooling _Instance;
    public static ObjectPooling Instance => _Instance;

    private Dictionary<GameObject, List<GameObject>> _pools = new();

    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
        if (_Instance.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
        {
            Destroy(this.gameObject);
        }
    }
    public GameObject GetObject(GameObject ObjectPref, Vector3 pos)
    {
        if (_pools.ContainsKey(ObjectPref))
        {
            foreach (GameObject item in _pools[ObjectPref])
            {
                if (item.activeSelf)
                {
                    continue;
                }
                return item;
            }
            GameObject g = Instantiate(ObjectPref, pos, Quaternion.identity);
            _pools[ObjectPref].Add(g);
            g.SetActive(false);
            return g;
        }
        List<GameObject> newObjPool = new();
        GameObject g2 = Instantiate(ObjectPref, pos, Quaternion.identity);
        newObjPool.Add(g2);
        g2.SetActive(false);
        _pools.Add(ObjectPref, newObjPool);
        return g2;

    }
}
