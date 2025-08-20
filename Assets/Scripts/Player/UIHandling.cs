using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandling : MonoBehaviour
{

    public bool paused = false;
    [SerializeField] private GameObject pauseUI;

    // Start is called before the first frame update
    void Start()
    {
        //As this is a shooter, we should start locked.
        Cursor.lockState = CursorLockMode.Locked;
        pauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            PauseMenu();
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
        if (paused)
        {
            Cursor.lockState = CursorLockMode.Confined; //This keeps the cursor within the window while paused.
        }
        else if (!paused)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void QuitGame()
    {
        Application.Quit(); //it's probably wrong, but im using a wrapper because it makes it easy to call in the OnClick()
    }
}
