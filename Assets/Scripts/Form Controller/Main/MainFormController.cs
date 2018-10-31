using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFormController : MonoBehaviour
{
    private MainFormPanel activePanel;

    public Camera cam;
    public GameObject connectingPanel;
    public GameObject disconectingPanel;

    public MainFormPanel ActivePanel { get { return activePanel; } set { SetActivePanel(value); } }

    void Start()
    {
        ActivePanel = MainFormPanel.Connecting;
    }

    private void SetActivePanel(MainFormPanel panelName)
    {
        switch (panelName)
        {
            case MainFormPanel.Null:
                {
                    connectingPanel.SetActive(false);
                    disconectingPanel.SetActive(false);
                    cam.gameObject.SetActive(false);
                    break;
                }
            case MainFormPanel.Connecting:
                {
                    connectingPanel.SetActive(true);
                    disconectingPanel.SetActive(false);
                    cam.gameObject.SetActive(true);
                    break;
                }
            case MainFormPanel.Disconnecting:
                {
                    connectingPanel.SetActive(false);
                    disconectingPanel.SetActive(true);
                    cam.gameObject.SetActive(true);
                    break;
                }
        }
        activePanel = panelName;
    }
}
public enum MainFormPanel { Null, Connecting, Disconnecting }
