using ClientModel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MyClanPanel : MonoBehaviour
{
    public Text clanName;
    public Text dateOfcreation;
    public Text seasonalPoints;
    public Text leaderName;
    public Text participantCount;

    public ScrollRect participantScroll;
    public ParticipantScrollView participantScrollPrefab;

    private void Start()
    {
        if (MenuManager.AccountInfo.Clan != null)
            UpdateView();
    }

    public void UpdateView()
    {
        clanName.text += MenuManager.AccountInfo.Clan.Name;
        dateOfcreation.text += MenuManager.AccountInfo.Clan.DateOfCreation.ToShortDateString();
        seasonalPoints.text += MenuManager.AccountInfo.Clan.SeasonalPoints.ToString();
        leaderName.text += MenuManager.AccountInfo.Clan.Leader.Nickname;
        participantCount.text += MenuManager.AccountInfo.Clan.Participants.Count;

        foreach (OtherAccount item in MenuManager.AccountInfo.Clan.Participants)
            Instantiate(participantScrollPrefab, new Vector3(), Quaternion.identity, participantScroll.content).participant = item;
    }
}