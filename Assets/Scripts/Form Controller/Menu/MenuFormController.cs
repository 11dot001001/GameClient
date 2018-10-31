using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFormController : MonoBehaviour
{
    private MenuFormContentType menuFormContentType;
    public MenuFormContentType MenuFormContentType { get { return menuFormContentType; } set { SetNewMenuForm(value); menuFormContentType = value; } }

    public MainPanelController mainPanel;
    public StartPanelController startPanel;
    public ShopPanelController shopPanel;
    public InventoryPanelController inventoryPanel;
    public CharacteristicsPanelController characteristicsPanel;
    public StatisticsPanelController statisticsPanel;
    public ClanPanelController clanPanel;

    private void SetNewMenuForm(MenuFormContentType menuForm)
    {
        switch (menuForm)
        {
            case MenuFormContentType.Start:
                {
                    mainPanel.gameObject.SetActive(true);
                    startPanel.gameObject.SetActive(true);
                    shopPanel.gameObject.SetActive(false);
                    inventoryPanel.gameObject.SetActive(false);
                    characteristicsPanel.gameObject.SetActive(false);
                    statisticsPanel.gameObject.SetActive(false);
                    clanPanel.gameObject.SetActive(false);
                    break;
                }
            case MenuFormContentType.Shop:
                {
                    mainPanel.gameObject.SetActive(true);
                    startPanel.gameObject.SetActive(false);
                    shopPanel.gameObject.SetActive(true);
                    inventoryPanel.gameObject.SetActive(false);
                    characteristicsPanel.gameObject.SetActive(false);
                    statisticsPanel.gameObject.SetActive(false);
                    clanPanel.gameObject.SetActive(false);
                    break;
                }
            case MenuFormContentType.Inventory:
                {
                    mainPanel.gameObject.SetActive(true);
                    startPanel.gameObject.SetActive(false);
                    shopPanel.gameObject.SetActive(false);
                    inventoryPanel.gameObject.SetActive(true);
                    characteristicsPanel.gameObject.SetActive(false);
                    statisticsPanel.gameObject.SetActive(false);
                    clanPanel.gameObject.SetActive(false);
                    break;
                }
            case MenuFormContentType.Characteristics:
                {
                    mainPanel.gameObject.SetActive(true);
                    startPanel.gameObject.SetActive(false);
                    shopPanel.gameObject.SetActive(false);
                    inventoryPanel.gameObject.SetActive(false);
                    characteristicsPanel.gameObject.SetActive(true);
                    statisticsPanel.gameObject.SetActive(false);
                    clanPanel.gameObject.SetActive(false);
                    break;
                }
            case MenuFormContentType.Statistics:
                {
                    mainPanel.gameObject.SetActive(true);
                    startPanel.gameObject.SetActive(false);
                    shopPanel.gameObject.SetActive(false);
                    inventoryPanel.gameObject.SetActive(false);
                    characteristicsPanel.gameObject.SetActive(false);
                    statisticsPanel.gameObject.SetActive(true);
                    clanPanel.gameObject.SetActive(false);
                    break;
                }
            case MenuFormContentType.Clan:
                {
                    mainPanel.gameObject.SetActive(true);
                    startPanel.gameObject.SetActive(false);
                    shopPanel.gameObject.SetActive(false);
                    inventoryPanel.gameObject.SetActive(false);
                    characteristicsPanel.gameObject.SetActive(false);
                    statisticsPanel.gameObject.SetActive(false);
                    clanPanel.gameObject.SetActive(true);
                    break;
                }
            default:
                break;
        }

    }
}
public enum MenuFormContentType { Start, Shop, Inventory, Characteristics, Statistics, Clan }
