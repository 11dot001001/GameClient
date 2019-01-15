using GameCore.Enums;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Bacterium : MonoBehaviour
{
    private const float _bacteriumGrowthTimerInterval = 1000f;
    private DateTime _lastBacteriumGrowth;
    private bool _isSelect;
    public int Id;

    public Material[] Materials;
    public LineRenderer LineRenderer;
    public Text VirusCountText;

    public BacteriumModel BacteriumModel { get; set; }
    public event EventHandler MouseDown;

    private void Awake()
    {
        LineRenderer = GetComponent<LineRenderer>();
        _lastBacteriumGrowth = DateTime.Now;
    }
    private void Update()
    {
        VirusCountText.text = BacteriumModel.VirusCount.ToString();
        if (_isSelect)
            WriteLine();
    }
    private void OnMouseDown()
    {
        //if (owner == OwnerType.My)
        //{
        MouseDown?.Invoke(this, EventArgs.Empty);
        LineRenderer.enabled = true;
        _isSelect = true;
        //}
    }
    private void OnMouseEnter()
    {
        if (BacteriumModel.Owner != OwnerType.Enemy && GameManager.SelcetedMode())
        {
            MouseDown?.Invoke(this, EventArgs.Empty);
            LineRenderer.enabled = true;
            _isSelect = true;
        }
    }
    private void ChangeColor(OwnerType owner)
    {
        switch (owner)
        {
            case OwnerType.My:
            {
                gameObject.GetComponent<SpriteRenderer>().material = Materials[0];
                break;
            }

            case OwnerType.Friend:
            {

                break;
            }

            case OwnerType.Enemy:
            {
                gameObject.GetComponent<SpriteRenderer>().material = Materials[1];
                break;
            }
            case OwnerType.None:
            {
                break;
            }
        }
    }
    private void WriteLine() => LineRenderer.SetPositions(new Vector3[] { BacteriumModel.Transform.Position, GameManager.GetMousePosition() });

    public void UpdateBacterium() => BacteriumModel.VirusCount++;
    public void CleanLine()
    {
        _isSelect = false;
        LineRenderer.enabled = false;
    }
}

