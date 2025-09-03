using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownLook : MonoBehaviour
{
    //To retrieve settings, we have to scale up the hierarchy
    private Transform playerObj;
    private PlayerSettings settings;
    private float mouseSens, rotationTotal;
    [SerializeField] private UIHandling pauseCheck;
    
    // Start is called before the first frame update
    void Start()
    {
        playerObj = transform.parent;
        settings = playerObj.GetComponent<PlayerSettings>();
        mouseSens = PlayerPrefs.GetFloat("sensitivity");
    }

    // Update is called once per frame
    void Update()
    {
        //This check has been added to all code: it stops activity if "paused" is true.
        if (pauseCheck.paused)
        {
            return;
        }

        mouseSens = PlayerPrefs.GetFloat("sensitivity");

        float verticalInput = Input.GetAxis("Mouse Y");
        float rotationVal = verticalInput * mouseSens;

        rotationTotal += rotationVal;

        Debug.Log("vert sens:" + mouseSens);

        //Handle infinite rotation case
        if (rotationTotal > 90)
        {
            rotationTotal = 90;
            return;
        }
        else if (rotationTotal < -90)
        {
            rotationTotal = -90;
            return;
        }
        
        transform.Rotate(rotationVal, 0, 0);
    }
}
