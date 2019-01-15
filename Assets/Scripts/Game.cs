using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;

public class Game : MonoBehaviour
{
    private const float _bacteriumGrowthTimerInterval = 1000f;
    private Timer _bacteriumGrowthTimer;

    public Bacterium BacteriumPrefab;
    public GameObject VirusPrefab;
    public Camera CurrentCamera;
    public LineRenderer LineRenderer;
    public GameObject GameField;

    public List<Bacterium> Bacteriums = new List<Bacterium>();
    public List<Bacterium> SelectedBacteriums = new List<Bacterium>();

    public Vector2 MousePosition => CurrentCamera.ScreenToWorldPoint(Input.mousePosition);

    public void Start()
    {
        GameManager.Initialize(this);
        LineRenderer = GetComponent<LineRenderer>();
        _bacteriumGrowthTimer = new Timer(_bacteriumGrowthTimerInterval);
        _bacteriumGrowthTimer.Elapsed += _bacteriumGrowthTimer_Elapsed; ;
        _bacteriumGrowthTimer.Start();
    }

   private void _bacteriumGrowthTimer_Elapsed(object sender, ElapsedEventArgs e) => Bacteriums.ForEach(x => x.UpdateBacterium());
    private void Update()
    {
        if (SelectedBacteriums.Count == 0)
            return;

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Collider2D bacteriumColider = Physics2D.OverlapPoint(MousePosition);
            if (bacteriumColider != null)
            {
                Bacterium bacterium = bacteriumColider.GetComponent<Bacterium>();
                GameManager.RequestSendViruses(SelectedBacteriums.Select(x => x.Id), bacterium.Id);
            }
            foreach (Bacterium bacterium in SelectedBacteriums)
                bacterium.CleanLine();
            SelectedBacteriums.Clear();
        }
    }

    public void OnSelectedBacterium(object sender, EventArgs e)
    {
        if (!SelectedBacteriums.Contains((Bacterium)sender))
            SelectedBacteriums.Add((Bacterium)sender);
    }
}
