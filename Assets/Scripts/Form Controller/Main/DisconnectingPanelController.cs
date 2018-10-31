using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Forms_Controller.Startup_Form
{
    public class DisconnectingPanelController : MonoBehaviour
    {
        public void ConnectionButtonClick()
        {
            MainManager.SetActivePanel(MainFormPanel.Connecting);
            MainManager.TryToConnect();
        }
    }
}
