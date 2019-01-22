using Assets.Scripts.Game;
using GameCore.Model;
using GameCore.Tools;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameManager
{
    private const float _virusSpeed = 1f;
    private static Game _game;
    private static List<BacteriumData> _bacteriumsData;
    private static List<VirusGroup> _virusGroups;
    public static GameObject VirusPrefab => _game.VirusPrefab;
    public static RoadManager RoadManager;

    public static void RequestSendViruses(IEnumerable<int> bacteriumsId, int targetID) => Network.RequestSendViruses(bacteriumsId, targetID);
    public static void SendVirusGroup(VirusGroupData virusGroupData, int newVirusCount)
    {
        Bacterium startBacterium = _game.Bacteriums.First(x => x.Id == virusGroupData.StartBacteriumId);
        Bacterium endBacterium = _game.Bacteriums.First(x => x.Id == virusGroupData.EndBacteriumId);
        Road road = RoadManager.GetRoad(startBacterium.BacteriumModel, endBacterium.BacteriumModel, virusGroupData.RoadId);
        _virusGroups.Add(new VirusGroup(road, startBacterium.BacteriumModel.VirusCount - newVirusCount, _virusSpeed));
        startBacterium.BacteriumModel.VirusCount = newVirusCount;
    } 
    public static void SendVirusGroupArrived(int bacteriumId, int newVirusCount)
    {
        Bacterium bacterium = _game.Bacteriums.First(x => x.Id == bacteriumId);
        bacterium.BacteriumModel.VirusCount = newVirusCount;
    }
    public static Vector2 GetMousePosition() => _game.MousePosition;
    public static bool SelcetedMode() => _game.SelectedBacteriums.Count != 0;
    public static void Initialize(Game game)
    {
        _game = game;
        foreach (BacteriumData bacteriumData in _bacteriumsData)
        {
            Bacterium current;
            game.Bacteriums.Add(current = Object.Instantiate(game.BacteriumPrefab, bacteriumData.Transform.Position, Quaternion.identity, game.GameField.transform));
            current.BacteriumModel = new BacteriumModel(bacteriumData);
            current.MouseDown += game.OnSelectedBacterium;
        }
        RoadManager = new RoadManager(_game.Bacteriums.Select(x=> x.BacteriumModel));
    }
    public static void Instantiate(GameSettings gameSettings)
    {
        _bacteriumsData = new List<BacteriumData>(gameSettings.Bacteriums);
        _virusGroups = new List<VirusGroup>();
        MenuManager.StartGame();
    }
}
