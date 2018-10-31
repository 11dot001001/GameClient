using Assets.Scripts.Data;
using Assets.Scripts.Forms_Controller.Message_Box_Form;
using Assets.Scripts.Forms_Controller.Startup_Form;
using Assets.Scripts.Managers;
using ClientModel.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents a main class for cynchronization.
/// </summary>
public static class Main
{
    /// <summary>
    /// Represents a variable that indicates the state of quit.
    /// </summary>
    private static bool _isQuit;

    public static MessageBoxFormController MessageBoxFormPrefab;
    public static SynchronizationContext SynchronizationContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="Main"/> class.
    /// </summary>
    /// <param name="launcher">A reference on the launcher.</param>
    public static void Initialize(Launcher launcher)
    {
        MessageBoxFormPrefab = launcher.messageBoxFormPrefab;
        launcher.ApplicationQuit += OnApplicationQuit;

        SynchronizationContext = SynchronizationContext.Current;
        if (SynchronizationContext == null)
            throw new NullReferenceException();
        
        Network.Initialize("127.0.0.1", 25000);
        Network.Connected += Client_ConnectedAsync;
        Network.FailedToConnect += Client_FailedToConnectAsync;
        Network.Disconnected += Client_DisconnectedAsync;

        Start();
    }

    public static void Start()
    {
        DataModel.Reset();
        Network.Start();
    }

    /// <summary>
    /// Occurs when the <see cref="Launcher"/> finishes its work.
    /// </summary>
    private static void OnApplicationQuit()
    {
        _isQuit = true;
        Network.Stop();
    }

    /// <summary>
    /// Occurs when the <see cref="Client"/> connected.
    /// </summary>
    private static void Client_ConnectedAsync() => SynchronizationContext.Post(Client_Connected, null);
    /// <summary>
    /// Occurs when the occurs <seealso cref="Client_ConnectedAsync"/>. 
    /// </summary>
    private static void Client_Connected(object obj)
    {
        MainManager.SetActivePanel(MainFormPanel.Null);
        SceneController.ChangeScene(SceneCode.Authentication);
    }
    /// <summary>
    /// Occurs when the <see cref="Client"/> failedToConnect.
    /// </summary>
    private static void Client_FailedToConnectAsync() => SynchronizationContext.Post(Client_FailedToConnect, null);
    /// <summary>
    /// Occurs when the occurs <seealso cref="Client_FailedToConnectAsync"/>. 
    /// </summary>
    private static void Client_FailedToConnect(object obj)
    {
        MainManager.SetActivePanel(MainFormPanel.Disconnecting);
        SceneController.ChangeScene(SceneCode.Null);
    }
    /// <summary>
    /// Occurs when the <see cref="Client"/> disconnected.
    /// </summary>
    private static void Client_DisconnectedAsync()
    {
        if (_isQuit)
            return;
        SynchronizationContext.Post(Client_Disconnected, null);
    }
    /// <summary>
    /// Occurs when the occurs <seealso cref="Client_DisconnectedAsync"/>.
    /// </summary>
    private static void Client_Disconnected(object obj)
    {
        MainManager.SetActivePanel(MainFormPanel.Disconnecting);
        SceneController.ChangeScene(SceneCode.Null);
    }
}