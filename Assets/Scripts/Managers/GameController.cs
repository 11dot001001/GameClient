using GameCore.Model;
using GameCore.Tools;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameController
{
    private const float _virusSpeed = 1f;
    private static Game _game;
    private static List<BacteriumData> _bacteriums;

    public static void RequestSendViruses(IEnumerable<int> bacteriumsId, int targetID) => Network.RequestSendViruses(bacteriumsId, targetID);
    public static void SendVirusGroup(VirusGroupData virusGroupData)
    {
        Bacterium startBacterium = _game.Bacteriums.First(x => x.Id == virusGroupData.StartBacteriumId);
        Virus virus = Object.Instantiate(_game.VirusPrefab, startBacterium.transform.position, Quaternion.identity).GetComponent<Virus>();
        Road road = startBacterium.BacteriumModel.Roads.First(x => x.Key == virusGroupData.EndBacteriumId).Value.Roads[virusGroupData.RoadId];
        virus.Initialize(road, _virusSpeed);
    }
    public static Vector2 GetMousePosition() => _game.MousePosition;
    public static bool SelcetedMode() => _game.SelectedBacteriums.Count != 0;
    public static void Initialize(Game game)
    {
        _game = game;
        int id = 0;
        foreach (BacteriumData bacteriumData in _bacteriums)
        {
            Bacterium current;
            game.Bacteriums.Add(current = Object.Instantiate(game.BacteriumPrefab, bacteriumData.Transform.Position, Quaternion.identity, game.GameField.transform));
            current.Id = id++;
            current.BacteriumModel = new BacteriumModel(_bacteriums.Count, bacteriumData);
            current.MouseDown += game.OnSelectedBacterium;
        }
        for (int i = 0; i < _bacteriums.Count; i++)
            for (int j = 0; j < _bacteriums.Count; j++)
                if (j != i)
                    _game.Bacteriums[i].BacteriumModel.Roads.Add(_bacteriums[j].Id, new Path(_game.Bacteriums[i].BacteriumModel, new List<Road>(new RoadManager(_game.Bacteriums[i].BacteriumModel, _game.Bacteriums[j].BacteriumModel, _game.Bacteriums.Select(x => x.BacteriumModel)).Roads)));
    }
    public static void Instantiate(GameSettings gameSettings)
    {
        _bacteriums = new List<BacteriumData>(gameSettings.Bacteriums);
        MenuManager.StartGame();
    }
}
