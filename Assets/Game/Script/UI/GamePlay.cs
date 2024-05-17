using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : UICanvas
{
    [SerializeField] TextMeshProUGUI enemyNumber;
    // Start is called before the first frame update
    void Start()
    {
        Observer.Instance.AddListener(CONSTANT.SET_ENEMY_TEXT,GetTotalEmeny);
    }

   private void GetTotalEmeny(object count)
    {
        enemyNumber.text = "" + (int)count;
    }
}
