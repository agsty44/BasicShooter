using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    //Retrieve settings
    private PlayerSettings settings;
    private float mouseSens;

    // Start is called before the first frame update
    void Start()
    {
        settings = GetComponent<PlayerSettings>();
        mouseSens = settings.mouseSens;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Mouse X");

        float rotationVal = horizontalInput * mouseSens;

        transform.Rotate(0, rotationVal, 0);
    }
}
