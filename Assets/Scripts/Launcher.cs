using Assets.Scripts.Forms_Controller.Message_Box_Form;
using Assets.Scripts.Forms_Controller.Startup_Form;
using Assets.Scripts.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Represent a laucher.
/// </summary>
public class Launcher : MonoBehaviour
{
    public MessageBoxFormController messageBoxFormPrefab;
    public MainFormController mainFormController;

    /// <summary>
    /// Occurs when the <see cref="Launcher"/> quit.
    /// </summary>
    public event Action ApplicationQuit;

    private void Start()
    {
        SceneController.Initialize(SceneManager.GetActiveScene());
        MainManager.Initialize(mainFormController);
        Main.Initialize(this);
    }
    
    private void OnApplicationQuit()
    {
        ApplicationQuit?.Invoke();
    }
}
