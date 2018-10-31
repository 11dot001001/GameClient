using GameCore;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

namespace Assets.Scripts.Forms_Controller.Message_Box_Form
{
    public class MessageBoxFormController : MonoBehaviour
    {
        public Text message;
        public Button button;

        public void Initialize(string messageText, string buttonText, Action action)
        {
            message.text = messageText;
            button.gameObject.GetComponentInChildren<Text>().text = buttonText;
        }

        public void OnButtonClick()
        {
            Destroy(gameObject);
        }

        public void Initialize(string messageText, string buttonText)
        {
            message.text = messageText;
            button.gameObject.GetComponentInChildren<Text>().text = buttonText;
        }
    }
}
