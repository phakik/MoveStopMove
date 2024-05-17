using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponItemUI : MonoBehaviour
{
    [SerializeField] private Image weaponImage;
    [SerializeField] private Button weaponButton;
    void Init(Sprite sprite, Action click)
    {
        weaponImage.sprite = sprite;
        weaponButton.onClick.AddListener(() =>
        {
            click.Invoke();
        });
    }
}
