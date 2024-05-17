using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LoadUnitData : MonoBehaviour
{
    private static LoadUnitData _Instance;
    public static LoadUnitData Instance => _Instance;
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
    public PlayerData LoadData()
    {
        string dataJSON = PlayerPrefs.GetString(CONSTANT.KEY_SAVEUNITDATA);
        if (string.IsNullOrEmpty(dataJSON))
        {
            return new("phuc", 1, Enum.WeaponType.candy_1, new List<int> { 0, 0, 1, 0, 1 }, Enum.PantType.chambi, new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, });
        }
        return JsonUtility.FromJson<PlayerData>(dataJSON);
    }
    public void SaveUnitData(PlayerData data)
    {
        string dataString = JsonUtility.ToJson(data);//doi tu UnitData sang string
        PlayerPrefs.SetString(CONSTANT.KEY_SAVEUNITDATA, dataString);
    }
    public void DeleteUnitData(PlayerData data)
    {
        string dataString = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(CONSTANT.KEY_SAVEUNITDATA, dataString);
    }


}