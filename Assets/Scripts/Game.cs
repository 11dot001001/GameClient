using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Bacterium BacteriumPrefab;
    public GameObject VirusPrefab;
    public Camera CurrentCamera;
    public LineRenderer LineRenderer;

    public List<Bacterium> Bacteriums = new List<Bacterium>();
    public List<Bacterium> SelectedBacteriums = new List<Bacterium>();

    public Vector2 MousePosition => CurrentCamera.ScreenToWorldPoint(Input.mousePosition);
    public event EventHandler ResetSelectedBacterium;

    public void Start()
    {
        GameController.Initialize(this);
        LineRenderer = GetComponent<LineRenderer>();
    }

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
                Network.GetViruses(SelectedBacteriums.Select(x => x.Id), bacterium.Id);
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
