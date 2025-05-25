using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const string START_MENU = "StartMenu";
    private const string GAME_SCENE = "GameScene";
    private const string GAME_OVER_MENU = "GameOverMenu";

    private readonly Dictionary<string, int> scenes = new(); // Scene name and Build Index

    private void Awake()
    {
        scenes.Add(START_MENU, 0);
        scenes.Add(GAME_SCENE, 1);
        scenes.Add(GAME_OVER_MENU, 2);
    }

    #region Load Methods

    public void LoadStartMenu() => LoadScene(scenes[START_MENU]);
    public void LoadGameScene() => LoadScene(scenes[GAME_SCENE]);
    public void LoadGameOverMenu() => LoadScene(scenes[GAME_OVER_MENU]);
    private void LoadScene(int index) => SceneManager.LoadScene(index);

    #endregion
}
