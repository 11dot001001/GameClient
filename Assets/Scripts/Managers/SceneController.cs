using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public static class SceneController
{
    public static Scene MainScene { get; private set; }

    public static Scene CurrentScene { get; set; }

    public static void Initialize(Scene mainScene)
    {
        MainScene = mainScene;
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private static void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        CurrentScene = arg0;
        SceneManager.SetActiveScene(CurrentScene);
    }

    public static void ChangeScene(SceneCode newScene)
    {
        if(CurrentScene.buildIndex != -1)
            SceneManager.UnloadSceneAsync(CurrentScene);

        if(newScene == SceneCode.Null)
        {
            CurrentScene = new Scene();
            return;
        }
        SceneManager.LoadScene((int)newScene, LoadSceneMode.Additive);
    }
}
public enum SceneCode
{
    Null,
    Authentication,
    Menu,
    Game
}

