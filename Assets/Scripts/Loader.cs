using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class Loader 
{
    public enum Scene
    {
        Game_Scene,
        LoadingScene,
        Main_Menu,
    }

    private static Scene targetScene;
    public static void LoadScene(Scene scene)
    { 
            SceneManager.LoadScene(Scene.LoadingScene.ToString());
   
     
        targetScene = scene;
    }

    public static void LoadGameScene_afterLoadingScene()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
