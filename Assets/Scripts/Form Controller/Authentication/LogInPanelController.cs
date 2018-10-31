using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Forms_Controller.Startup_Form
{
    public class LogInPanelController : MonoBehaviour
    {
        private bool isEmailExist;

        public Text email;
        public Text password;

        public event EventHandler OnExitButtonClick;

        public void ExitButtonClick()
        {
            OnExitButtonClick.Invoke(this, EventArgs.Empty);
        }
        public void LogInButtonClick()
        {
            if (!isEmailExist)
                return;
            AuthenticationManager.LogIn(email.text, password.text);
        }
        public void EndEditEmail()
        {
            isEmailExist = false;
            if (email.text.Length == 0)
            {
                return;
            }
            isEmailExist = true;
        }
    }
}
