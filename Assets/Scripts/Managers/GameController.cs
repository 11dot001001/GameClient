using ClientModel.Model;
using GameCore.Model;
using GameCore.Tools;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameController
{
    private static Game _game;
    private static List<BacteriumData> _bacteriums;

    public static void SendVirus(IEnumerable<int> bacteriums, int targetId, int count)
    {
        //Bacterium target = _game.Bacteriums.First(x => x.Id == targetId);
        //foreach (int bacteriumId in bacteriums)
        //{
        //    Bacterium bacterium = _game.Bacteriums.First(x => x.Id == bacteriumId);
        //    for (int i = 0; i < bacterium.BacteriumModel.Roads.First(x => x.Key == targetId).Value.Roads.Count; i++)
        //    {
        //        Virus virus = Object.Instantiate(_game.VirusPrefab, bacterium.transform.position, Quaternion.identity).GetComponent<Virus>();
        //        virus.Initialize(bacterium.BacteriumModel.Roads.First(x=> x.Key == targetId).Value.Roads[i], 1f);
        //    }
        //}
    }
    public static void SendVirusGroup(VirusGroupNet virusGroup)
    {
        Bacterium bacterium = _game.Bacteriums.First(x => x.Id == virusGroup.StartId);
        //Virus virus = Object.Instantiate(_game.VirusPrefab, bacterium.transform.position, Quaternion.identity).GetComponent<Virus>();
        //virus.Initialize(bacterium.BacteriumModel.Roads.First(x=> x.Key == virusGroup.EndId).Value.Roads[virusGroup.RoadId], virusGroup.Speed);

        for (int i = 0; i < bacterium.BacteriumModel.Roads.First(x => x.Key == virusGroup.EndId).Value.Roads.Count; i++)
        {
            if(bacterium.BacteriumModel.Roads.First(x => x.Key == virusGroup.EndId).Value.Roads[i].EaseFactor == 1f)
            {
                Virus virus = Object.Instantiate(_game.VirusPrefab, bacterium.transform.position, Quaternion.identity).GetComponent<Virus>();
                virus.Initialize(bacterium.BacteriumModel.Roads.First(x => x.Key == virusGroup.EndId).Value.Roads[i], 1f);
            }
        }
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
            game.Bacteriums.Add(current = Object.Instantiate(game.BacteriumPrefab, bacteriumData.Transform.Position, Quaternion.identity));
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
