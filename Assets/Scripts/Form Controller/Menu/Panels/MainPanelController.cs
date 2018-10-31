using ClientModel.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelController : MonoBehaviour
{
    public Button start;
    public Button shop;
    public Button inventory;
    public Button characteristics;
    public Button statistics;
    public Button clan;

    public Text nicknameText;
    public Text levelText;
    public Text moneyText;

    public void Start()
    {
        start.onClick.AddListener(StartButtonClick);
        shop.onClick.AddListener(ShopButtonClick);
        inventory.onClick.AddListener(InventoryButtonClick);
        characteristics.onClick.AddListener(CharacteristicsButtonClick);
        statistics.onClick.AddListener(StatisticsButtonClick);
        clan.onClick.AddListener(ClanButtonClick);

        nicknameText.text = MenuManager.AccountInfo.Nickname;
        levelText.text += MenuManager.AccountInfo.Level;
        moneyText.text += MenuManager.AccountInfo.Money.ToString();
    }

    public void StartButtonClick()
    {
        MenuManager.SetNewMenuContent(MenuFormContentType.Start);
    }
    public void ShopButtonClick()
    {
        MenuManager.SetNewMenuContent( MenuFormContentType.Shop);
    }
    public void InventoryButtonClick()
    {
        MenuManager.SetNewMenuContent(MenuFormContentType.Inventory);
    }
    public void CharacteristicsButtonClick()
    {
        MenuManager.SetNewMenuContent(MenuFormContentType.Characteristics);
    }
    public void StatisticsButtonClick()
    {
        MenuManager.SetNewMenuContent(MenuFormContentType.Statistics);
    }
    public void ClanButtonClick()
    {
        MenuManager.SetNewMenuContent(MenuFormContentType.Clan);
    }
}
