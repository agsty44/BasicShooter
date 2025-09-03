using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandling : MonoBehaviour
{

    [SerializeField] private GameObject levelList, settingsPage, playButtonScreen;

    // Start is called before the first frame update
    void Start()
    {
        playButtonScreen.SetActive(true);
        settingsPage.SetActive(false);
        levelList.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPlayButtonClick()
    {
        playButtonScreen.SetActive(false);
        levelList.SetActive(true);
    }

    public void OnReturnToPlayScreen()
    {
        playButtonScreen.SetActive(true);
        levelList.SetActive(false);
    }

    public void OpenLevel(string sceneName)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }
}
