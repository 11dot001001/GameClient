using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsPanelController : MonoBehaviour
{
    public Text nickname;
    public Text level;
    public Text levelPoints;
    public Text accountStatus;
    public Text money;
    public Text seasonalPoints;
    public Text victories;
    public Text losses;
    public Text clan;
    public Text dateOfMembership;
    public Text clanPoints;

    public void Start()
    {
        UpdateView();
    }

    public void UpdateView()
    {
        nickname.text += MenuManager.AccountInfo.Nickname;
        level.text += MenuManager.AccountInfo.Level.ToString();
        levelPoints.text += MenuManager.AccountInfo.LevelPoints.ToString();
        accountStatus.text += MenuManager.AccountInfo.AccountStatus.ToString();
        money.text += MenuManager.AccountInfo.Money.ToString();
        seasonalPoints.text += MenuManager.AccountInfo.SeasonalPoints.ToString();
        victories.text += MenuManager.AccountInfo.Victories.ToString();
        losses.text += MenuManager.AccountInfo.Losses.ToString();
        clan.text += MenuManager.AccountInfo.Clan.Name;
        dateOfMembership.text += MenuManager.AccountInfo.DateOfMembership.ToShortDateString();
        clanPoints.text += MenuManager.AccountInfo.ClanPoints.ToString();
    }
}
