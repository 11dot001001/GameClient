using GameCore.Model;

public class BacteriumModel : BacteriumBase
{
    public BacteriumModel(int roadsCount, BacteriumData bacteriumData) : base(roadsCount, bacteriumData.Id, bacteriumData.Owner, bacteriumData.Transform, bacteriumData.VirusCount) { }
}

