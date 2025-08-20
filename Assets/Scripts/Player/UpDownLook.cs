using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownLook : MonoBehaviour
{
    //To retrieve settings, we have to scale up the hierarchy
    private Transform playerObj;
    private PlayerSettings settings;
    private float mouseSens;
    [SerializeField] private UIHandling pauseCheck;
    
    // Start is called before the first frame update
    void Start()
    {
        playerObj = transform.parent;
        settings = playerObj.GetComponent<PlayerSettings>();
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

        float verticalInput = Input.GetAxis("Mouse Y");
        float rotationVal = verticalInput * mouseSens;
        transform.Rotate(rotationVal, 0, 0);
    }
}
