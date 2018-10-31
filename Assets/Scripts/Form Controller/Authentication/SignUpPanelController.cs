using GameCore;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Forms_Controller.Startup_Form
{
    public class SignUpPanelController : MonoBehaviour
    {
        private bool isEmaiTrue = false;
        private bool isPasswordTrue = false;
        private bool isNicknameTrue = false;

        public Text email;
        public Text password;
        public Text nickname;

        public Text emailEnterHelper;
        public Text passwordEnterHelper;
        public Text nicknameEnterHelper;

        public event EventHandler OnExitButtonClick;

        private void Awake()
        {
            AuthenticationManager.IsEmailExistsResponseInvoke += ReplyIsEmailExistsInvoke;
            AuthenticationManager.IsNicknameExistsResponseInvoke += ReplyIsNicknameExistsInvoke;
        }

        private void SetErrorString(Text textHelper, SignUpErrors error)
        {
            switch (error)
            {
                case SignUpErrors.Null:
                    {
                        textHelper.text = string.Empty;
                        break;
                    }
                case SignUpErrors.EmailIsNotValid:
                    {
                        textHelper.text = "Email isn't valid";
                        break;
                    }
                case SignUpErrors.EmailIsExists:
                    {
                        textHelper.text = "Email exists";
                        break;
                    }
                case SignUpErrors.PasswordIsNotValid:
                    {
                        textHelper.text = "Password isn't valid";
                        break;
                    }
                case SignUpErrors.NicknameIsNotValid:
                    {
                        textHelper.text = "Nickname isn't valid";
                        break;
                    }
                case SignUpErrors.NicknameIsExists:
                    {
                        textHelper.text = "Nickname exists";
                        break;
                    }
            }
        }

        public void ReplyIsEmailExistsInvoke(object sender, IsEmailExistsEventArgs e)
        {
            if (e.Answer)
            {
                SetErrorString(emailEnterHelper, SignUpErrors.EmailIsExists);
                return;
            }
            isEmaiTrue = true;
        }
        public void ReplyIsNicknameExistsInvoke(object sender, IsNicknameExistsEventArgs e)
        {
            if (e.Answer)
            {
                SetErrorString(nicknameEnterHelper, SignUpErrors.NicknameIsExists);
                return;
            }
            isNicknameTrue = true;
        }

        public void ExitButtonClick()
        {
            OnExitButtonClick.Invoke(this, new EventArgs());
        }
        public void SignUpButtonClick()
        {
            if (!isEmaiTrue || !isPasswordTrue || !isNicknameTrue)
                return;
            AuthenticationManager.SignUp(email.text, password.text, nickname.text);
        }

        public void ValueChangedEmail()
        {
            SetErrorString(emailEnterHelper, SignUpErrors.Null);
        }
        public void ValueChangedPassword()
        {
            SetErrorString(passwordEnterHelper, SignUpErrors.Null);
        }
        public void ValueChangedNickname()
        {
            SetErrorString(nicknameEnterHelper, SignUpErrors.Null);
        }

        public void EndEditEmail()
        {
            isEmaiTrue = false;
            if (email.text.Length == 0)
            {
                SetErrorString(emailEnterHelper, SignUpErrors.Null);
                return;
            }
            if (!Validation.IsEmailValid(email.text))
            {
                SetErrorString(emailEnterHelper, SignUpErrors.EmailIsNotValid);
                return;
            }
            AuthenticationManager.CheckEmailExists(email.text);
        }
        public void EndEditPassword()
        {
            isPasswordTrue = false;
            if (password.text.Length == 0)
            {
                SetErrorString(passwordEnterHelper, SignUpErrors.Null);
                return;
            }
            if (!Validation.IsPasswordValid(password.text))
            {
                SetErrorString(passwordEnterHelper, SignUpErrors.PasswordIsNotValid);
                return;
            }
            isPasswordTrue = true;
        }
        public void EndEditNickname()
        {
            isNicknameTrue = false;
            if (nickname.text.Length == 0)
            {
                SetErrorString(nicknameEnterHelper, SignUpErrors.Null);
                return;
            }
            if (!Validation.IsNicknameValid(nickname.text))
            {
                SetErrorString(nicknameEnterHelper, SignUpErrors.NicknameIsNotValid);
                return;
            }
            AuthenticationManager.CheckNickameExists(nickname.text);
        }

        private enum SignUpErrors
        {
            Null,
            EmailIsNotValid,
            EmailIsExists,
            PasswordIsNotValid,
            NicknameIsNotValid,
            NicknameIsExists,
        }
    }
}