using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : UICanvas
{
    public int itemIndex = 0;
    [SerializeField] RawImage itemImage;
    [SerializeField] Texture weaponTexture;
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] PantSO pantSO;
    [SerializeField] Button nextButton;
    [SerializeField] Button previousButton;
    [SerializeField] Button buyButton;
    [SerializeField] Button pantButton;
    [SerializeField] Button skinButton;
    [SerializeField] Button weaponButton;
    [SerializeField] Text buttonText;
    [SerializeField] Character player;
    Button lastestButton;
    PlayerData playerData;
    List<int> itemData;
    GameObject weapon;
    Sprite pantAva;

    private void Start()
    {
        playerData = LoadUnitData.Instance.LoadData();
        lastestButton = weaponButton;
        LoadWeapon();
        GetNextItem();
        GetPreviousItem();
        BuyWeapon();
        ButtonClicked();
    }
    private void LoadPant()
    {
        pantAva = pantSO.pantItemDataList[itemIndex].pantAvatar;
        itemImage.texture = pantAva.texture;
        CheckBuyItem();
    }
    private void LoadWeapon()
    {
        itemImage.texture = weaponTexture;
        weapon = ObjectPooling.Instance.GetObject(weaponSO.weaponDataList[itemIndex].weaponAvatar, new Vector3(1000, 1000, 4.9f));
        weapon.SetActive(true);
        CheckBuyItem();
    }
    void GetNextItem()
    {

        nextButton.onClick.AddListener(() =>
        {
            DatalistType();
            if (itemIndex < itemData.Count - 1)
            {
                itemIndex += 1;
            }
            else
            {
                itemIndex = 0;
            }
            weapon.SetActive(false);
            if (itemData == playerData.weaponData)
            {
                LoadWeapon();
            }
            else if (itemData == playerData.pantsData)
            {
                LoadPant();
            }

        });
    }
    public void GetPreviousItem()
    {
        previousButton.onClick.AddListener(() =>
        {
            DatalistType();
            if (itemIndex > 0)
            {
                itemIndex -= 1;
            }
            else
            {
                if (itemData == playerData.weaponData)
                {
                    itemIndex = weaponSO.weaponDataList.Count - 1;
                }
                else if (itemData == playerData.pantsData)
                {
                    itemIndex = pantSO.pantItemDataList.Count - 1;
                }
            }
            weapon.SetActive(false);
            if (itemData == playerData.weaponData)
            {
                LoadWeapon();
            }
            else if (itemData == playerData.pantsData)
            {
                LoadPant();
            }
        });
    }
    public void CheckBuyItem()
    {
        DatalistType();
        if (itemData[itemIndex] == 2)
        {
            buttonText.text = "Equip";
            buyButton.interactable = true;

        }
        else if (itemData[itemIndex] == 3)
        {
            buttonText.text = "Equipped";
            buyButton.interactable = false;
        }
        else
        {
            buttonText.text = "Buy";
            buyButton.interactable = true;
        }

    }
    public void ButtonClicked()
    {

        pantButton.onClick.AddListener(() =>
        {
            lastestButton = pantButton;
            LoadPant();
        });
        weaponButton.onClick.AddListener(() =>
        {
            lastestButton = weaponButton;
            itemIndex = 0;
            weapon.SetActive(false);
            LoadWeapon();
        });
    }
    public void BuyWeapon()
    {
        buyButton.onClick.AddListener(() =>
        {

            DatalistType();
            if (itemData[itemIndex] == 2)
            {
                itemData[itemIndex] = 3;
                if (itemData == playerData.weaponData)
                {
                    player.weapon = weaponSO.weaponDataList[itemIndex].weaponName;
                }
                else if(itemData == playerData.pantsData)
                {
                    player.pant = pantSO.pantItemDataList[itemIndex].pantName;
                }
                for (int i = 0; i < itemData.Count; i++)
                {
                    if (i == itemIndex)
                    {
                        continue;
                    }
                    if (itemData[i] == 3)
                    {
                        itemData[i] = 2;
                    }
                }
            }
            else if (itemData[itemIndex] == 3)
            {
                return;
            }
            else
            {
                itemData[itemIndex] = 2;
            }
            LoadUnitData.Instance.SaveUnitData(playerData);
            CheckBuyItem();
        });
    }
    void DatalistType()
    {
        if (lastestButton == weaponButton)
        {
            itemData = playerData.weaponData;
        }
        else if (lastestButton == pantButton)
        {
            itemData = playerData.pantsData;
        }
    }
    public void CLoseShopCanvas()
    {
        UIManager.Instance.CloseUI<Shop>(0f);
    }
}
