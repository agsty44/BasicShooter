using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    //Retrieve settings
    private PlayerSettings settings;
    private float mouseSens;
    [SerializeField] private UIHandling pauseCheck;

    // Start is called before the first frame update
    void Start()
    {
        settings = GetComponent<PlayerSettings>();
        mouseSens = settings.mouseSens;
    }

    public void SensChange()
    {
        mouseSens = settings.mouseSens;
    }

    // Update is called once per frame
    void Update()
    {
        //This check has been added to all code: it stops activity if "paused" is true.
        if (pauseCheck.paused)
        {
            return;
        }

        mouseSens = settings.mouseSens;

        float horizontalInput = Input.GetAxis("Mouse X");

        float rotationVal = horizontalInput * mouseSens;

        transform.Rotate(0, rotationVal, 0);

        Debug.Log("rotation sens:" + mouseSens);
    }
}
