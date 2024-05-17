using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : UICanvas
{
    private void Start()
    {
        UIManager.Instance.CloseUI<JoyStick>(0f);
        UIManager.Instance.CloseUI<GamePlay>(0f);
        LevelManager.Instance.DeleteMap();
    }
    public void MainMenuButton()
    {
        UIManager.Instance.OpenUI<MainMenu>();
        UIManager.Instance.CloseUI<Lose>(0f);

    }

}
