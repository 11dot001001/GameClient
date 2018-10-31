using Assets.Scripts;
using Assets.Scripts.Data;
using Assets.Scripts.Forms_Controller.Startup_Form;
using Assets.Scripts.Managers;
using ClientModel;
using ClientModel.Data;
using GameCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents a class of work with authorization.
/// </summary>
public static class AuthenticationManager
{
    private static StartupFormController _startupForm;

    public static event EventHandler<IsEmailExistsEventArgs> IsEmailExistsResponseInvoke;
    public static event EventHandler<IsNicknameExistsEventArgs> IsNicknameExistsResponseInvoke;

    public static void SetActivePanel(StartupFormController.Panels panelName) => _startupForm.ActivePanel = panelName;

    public static void Initialize(StartupFormController startupFormPrefab)
    {
        _startupForm = GameObject.Instantiate(startupFormPrefab);
        SetActivePanel(StartupFormController.Panels.Startup);
    }

    public static void CheckEmailExists(string email) => Network.IsEmailExists(email);
    public static void CheckEmailExistsResponse(bool answer) => IsEmailExistsResponseInvoke?.Invoke(null, new IsEmailExistsEventArgs(answer));

    public static void CheckNickameExists(string nickname) => Network.IsNicknameExists(nickname);
    public static void CheckNickameExistsResponse(bool answer) => IsNicknameExistsResponseInvoke?.Invoke(null, new IsNicknameExistsEventArgs(answer));

    public static void SignUp(string email, string password, string nickname) => Network.SignUp(email, password, nickname);

    public static void SignUpResponse(byte result, OwnAccount account)
    {
        switch ((SignUpResultCode)result)
        {
            case SignUpResultCode.SignUpEmailExists:
                {
                    MessageProcessingManager.InvokeMessageBox("Specified email exists.");
                    break;
                }
            case SignUpResultCode.SignUpNicknameExists:
                {
                    MessageProcessingManager.InvokeMessageBox("Specified nickname exists.");
                    break;
                }
            case SignUpResultCode.SignUpSuccessfully:
                {
                    DataModel.Initialize(account);
                    SceneController.ChangeScene(SceneCode.Menu);
                    break;
                }
        }
    }

    public static void LogIn(string email, string password) => Network.LogIn(email, password);
    public static void LogInResponse(OwnAccount account)
    {
        if (account == null)
            MessageProcessingManager.InvokeMessageBox(MessageCode.LogInError);
        DataModel.Initialize(account);
        SceneController.ChangeScene(SceneCode.Menu);
    }
}
