using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIHandling : MonoBehaviour
{

    public bool paused = false;
    [SerializeField] private GameObject pauseUI, crosshair, ammocount, settingsUI, quitUI;
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private TextMeshProUGUI sensText;

    //What's the logic here?
    //Upon pressing escape: we should invert the following:
    //Presence of pauseUI
    //Lockstate.
    //The presence of the settingsUI should ALWAYS be set false after escape

    // Start is called before the first frame update
    void Start()
    {
        //As this is a shooter, we should start locked.
        Cursor.lockState = CursorLockMode.Locked;
        pauseUI.SetActive(false);
        settingsUI.SetActive(false);
        quitUI.SetActive(false);

        GameObject[] HUDparts = {crosshair, ammocount};

        foreach (GameObject UIelement in HUDparts)
        {
            UIelement.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape")) //check for pressing escape
        {
            PauseMenu(); //toggle pause menu
            settingsUI.SetActive(false); //close the settings UI
        }
    }

    public void PauseMenu()
    {
        //We need to set the mouse lock status to the INVERSE of the paused bool
        //So if paused is true, lockedmouse is false, vice versa.
        //We also need to set the visibility of the UI to the same as paused bool.

        Debug.Log("Pause Pressed!");

        paused ^= true;
        //Ok, what the fuck is this?
        //this is an XOR. so there are 2 cases:

        //case 1 - paused = true
        //as this is XOR, if both A and B are 1, we return 0, inverting the bool.

        //case 2 - paused = false
        //A is 0, B is 1. return 1. bool inverted.

        //Now we should handle changes to the UI which are made upon pause/unpause.
        //Do this with a function: it means we can have a "resume" function with a button.

        pauseUI.SetActive(paused);

        GameObject[] HUDparts = {crosshair, ammocount};

        foreach (GameObject UIelement in HUDparts)
        {
            UIelement.SetActive(!paused);
        }
        
        if (paused)
        {
            Cursor.lockState = CursorLockMode.Confined; //This keeps the cursor within the window while paused.
        }
        else if (!paused)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void OpenSettings()
    {
        pauseUI.SetActive(false);
        settingsUI.SetActive(true);
    }

    public void CloseSettings()
    {
        pauseUI.SetActive(true);
        settingsUI.SetActive(false);
    }

    public void ChangeSensText()
    {
        sensText.text = "Sensitivity: " + sensitivitySlider.value;
    }

    public void OpenQuitMenu()
    {
        pauseUI.SetActive(false);
        quitUI.SetActive(true);
    }

    public void CloseQuitMenu()
    {
        pauseUI.SetActive(true);
        quitUI.SetActive(false);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit(); //it's probably wrong, but im using a wrapper because it makes it easy to call in the OnClick()
    }
}
