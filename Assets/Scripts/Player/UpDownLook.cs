using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownLook : MonoBehaviour
{
    //To retrieve settings, we have to scale up the hierarchy
    private Transform playerObj;
    private PlayerSettings settings;
    private float mouseSens;
    
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
        float verticalInput = Input.GetAxis("Mouse Y");
        float rotationVal = verticalInput * mouseSens;
        transform.Rotate(rotationVal, 0, 0);
    }
}
