using System;
using UnityEngine;
namespace Assets.Scripts.Forms_Controller.Startup_Form
{
    public class StartupFormController : MonoBehaviour
    {
        private Panels activePanel;

        public GameObject startupPanel;
        public GameObject signUpPanel;
        public GameObject logInPanel;

        public Panels ActivePanel { get { return activePanel; } set { SetActivePanel(value); } }

        private void Start()
        {
            signUpPanel.GetComponent<SignUpPanelController>().OnExitButtonClick += StartupFormController_OnExitButtonClick;
            logInPanel.GetComponent<LogInPanelController>().OnExitButtonClick += StartupFormController_OnExitButtonClick;
        }

        private void SetActivePanel(Panels panelName)
        {
            switch (panelName)
            {
                case Panels.Null:
                    {
                        startupPanel.SetActive(false);
                        signUpPanel.SetActive(false);
                        logInPanel.SetActive(false);
                        break;
                    }
                case Panels.Startup:
                    {
                        startupPanel.SetActive(true);
                        signUpPanel.SetActive(false);
                        logInPanel.SetActive(false);
                        break;
                    }
                case Panels.SignUp:
                    {
                        startupPanel.SetActive(false);
                        signUpPanel.SetActive(true);
                        logInPanel.SetActive(false);
                        break;
                    }
                case Panels.LogIn:
                    {
                        startupPanel.SetActive(false);
                        signUpPanel.SetActive(false);
                        logInPanel.SetActive(true);
                        break;
                    }
            }
            activePanel = panelName;
        }

        private void StartupFormController_OnExitButtonClick(object sender, EventArgs e)
        {
            ActivePanel = Panels.Startup;
        }

        public void OnSignUpButtonClick()
        {
            ActivePanel = Panels.SignUp;
        }
        public void OnLogInButtonClick()
        {
            ActivePanel = Panels.LogIn;
        }

        public enum Panels { Null, Startup, SignUp, LogIn }
    }
}