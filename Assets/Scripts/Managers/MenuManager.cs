using System;
using System.Collections.Generic;
using ClientModel.Data;
using UnityEngine;

public static class MenuManager
{
    private static MenuFormController _menuForm;
    public static OwnAccount AccountInfo { get; private set; }

    public static void SetNewMenuContent(MenuFormContentType menuFormContentType) => _menuForm.MenuFormContentType = menuFormContentType;

    public static void Initialize(MenuFormController menuFormPrefab)
    {
        //AccountInfo = DataModel.AccountInfo;

        OtherAccount friend = new OtherAccount()
        {
            Nickname = "Boss",
            Level = 2,
            Id = 0,
            Losses = 55,
            Victories = 1,
            Clan = new OtherClan() { Id = 3, Name = "TopClan" }
        };
        OtherAccount friend2 = new OtherAccount()
        {
            Nickname = "Boss2",
            Level = 2,
            Id = 2,
            Losses = 55,
            Victories = 1,
            Clan = new OtherClan() { Id = 3, Name = "TopClan2s" }
        };
        OwnClan clan = new OwnClan() { Leader = friend, Id = 0, DateOfCreation = DateTime.Now, Name = "TopClan", SeasonalPoints = 20000, Participants = new HashSet<OtherAccount>() };
        clan.Participants.Add(friend);
        clan.Participants.Add(friend2);
        OwnAccount ownAccount = new OwnAccount
        {
            Id = 1,
            Nickname = "Leha.Monstr",
            Level = 5,
            Money = 5555,
            Losses = 1,
            Victories = 55,
            Friends = new HashSet<OtherAccount>(),
            Clan = clan,
            AccountStatus = GameCore.Model.AccountStatus.Priority,
            ClanPoints = 9999,
            LevelPoints = 10,
            SeasonalPoints = 50,
            DateOfMembership = DateTime.Now
        };
        AccountInfo = ownAccount;

        ownAccount.Friends.Add(friend);
        ownAccount.Friends.Add(friend2);
        _menuForm = GameObject.Instantiate(menuFormPrefab);
        SetNewMenuContent(MenuFormContentType.Start);
    }

    public static void StartGame() => SceneController.ChangeScene(SceneCode.Game);
}