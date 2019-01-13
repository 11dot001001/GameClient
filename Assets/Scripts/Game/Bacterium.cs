using System;
using GameCore.Enums;
using UnityEngine;
using UnityEngine.UI;

public class Bacterium : MonoBehaviour
{
    private BacteriumModel _bacteriumModel;

    private bool _isSelect;
    public int Id;

    public Material[] Materials;
    public LineRenderer LineRenderer;
    public Text VirusCountText;

    public BacteriumModel BacteriumModel { get { return _bacteriumModel; } set { _bacteriumModel = value; } }

    public event EventHandler MouseDown;

    private void Awake() => LineRenderer = GetComponent<LineRenderer>();
    private void Update()
    {
        VirusCountText.text = _bacteriumModel.Data.VirusCount.ToString();
        if (_isSelect)
        {
            WriteLine();
        }
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
        if (_bacteriumModel.Data.Owner != OwnerType.Enemy && GameController.SelcetedMode())
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
    private void WriteLine() => LineRenderer.SetPositions(new Vector3[] { _bacteriumModel.Transform.Position, GameController.GetMousePosition() });

    public void CleanLine()
    {
        _isSelect = false;
        LineRenderer.enabled = false;
    }
}

