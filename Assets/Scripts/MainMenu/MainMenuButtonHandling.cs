using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuButtonHandling : MonoBehaviour
{

    [SerializeField] private GameObject levelList, settingsPage, playButtonScreen;
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private TextMeshProUGUI sensText;

    // Start is called before the first frame update
    void Start()
    {
        playButtonScreen.SetActive(true);
        settingsPage.SetActive(false);
        levelList.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
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

    public void OnSettingsButtonClick()
    {
        playButtonScreen.SetActive(false);
        settingsPage.SetActive(true);
    }

    public void OnReturnToPlayScreen()
    {
        playButtonScreen.SetActive(true);
        levelList.SetActive(false);
        settingsPage.SetActive(false);
    }

    public void OpenLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SensUpdate()
    {
        sensText.text = "Sensitivity: " + sensitivitySlider.value;
        PlayerPrefs.SetFloat("sensitivity", sensitivitySlider.value);
    }

    public void QuitGame()
    {
        Application.Quit(); //it's probably wrong, but im using a wrapper because it makes it easy to call in the OnClick()
    }
}
