using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    //Customisable settings are put here (sensitivity, physics values.)
    public float mouseSens = 1.0f; //Mouse sensitivity multiplier.
    public float playerSpeed = 5.0f; //Velocity multiplier.
    public float gravConstant = 9.8f; //How fast is gravity?
    public float initialJumpVelo = 2.7f; //How fast should they jump?

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
