using Assets.Scripts.Data;
using ClientModel.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanelController : MonoBehaviour
{
    public ScrollRect friendScroll;
    public FriendScrollView friendScrollPrefab;
    public Button findGame;

    public void Start()
    {
        findGame.onClick.AddListener(FindGameButonClick);
        UpdateView();
    }

    private void FindGameButonClick()
    {
        Network.FindGame();
    }

    public void UpdateView()
    {
        foreach (OtherAccount item in MenuManager.AccountInfo.Friends)
           Instantiate(friendScrollPrefab, new Vector3(), Quaternion.identity, friendScroll.content).friend = item;
    }
}
