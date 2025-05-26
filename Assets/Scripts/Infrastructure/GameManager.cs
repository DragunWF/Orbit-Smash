using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const string START_MENU = "StartMenu";
    private const string GAME_SCENE = "GameScene";
    private const string GAME_OVER_MENU = "GameOverMenu";

    private const string START_TRANSITION = "Start";
    private const float TRANSITION_TIME = 1f;
    private Animator transitionAnimator;

    private readonly Dictionary<string, int> scenes = new(); // Scene name and Build Index

    private void Awake()
    {
        scenes.Add(START_MENU, 0);
        scenes.Add(GAME_SCENE, 1);
        scenes.Add(GAME_OVER_MENU, 2);

        transitionAnimator = GetComponent<Animator>();
    }

    #region Load Methods

    public void LoadStartMenu() => StartCoroutine(LoadScene(scenes[START_MENU]));
    public void LoadGameScene() => StartCoroutine(LoadScene(scenes[GAME_SCENE]));
    public void LoadGameOverMenu() => StartCoroutine(LoadScene(scenes[GAME_OVER_MENU]));

    private IEnumerator LoadScene(int index)
    {
        transitionAnimator.SetTrigger(START_TRANSITION);
        yield return new WaitForSeconds(TRANSITION_TIME);
        SceneManager.LoadScene(index);
    }

    #endregion
}
