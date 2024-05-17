using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    public void ShopButton()
    {
        UIManager.Instance.OpenUI<Shop>();
    }
    public void PlayButton()
    {
        UIManager.Instance.CloseUI<MainMenu>(0f);
        LevelManager.Instance.LoadMap("1");
        UIManager.Instance.OpenUI<GamePlay>();
        UIManager.Instance.OpenUI<JoyStick>();
    }
}
