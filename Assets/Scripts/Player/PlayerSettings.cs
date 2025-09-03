using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    //Customisable settings are put here (sensitivity, physics values.)
    public float playerSpeed = 5.0f; //Velocity multiplier.
    public float gravConstant = 9.8f; //How fast is gravity?
    public float initialJumpVelo = 2.7f; //How fast should they jump?
    [SerializeField] Slider sensSlider;

    // Start is called before the first frame update
    void Start()
    {
        //Check that the key for sensitivity exists already
        if (PlayerPrefs.HasKey("sensitivity"))
        {
            sensSlider.value = PlayerPrefs.GetFloat("sensitivity");
        }
        else
        {
            PlayerPrefs.SetFloat("sensitivity", 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SensitivityChange()
    {
        PlayerPrefs.SetFloat("sensitivity", sensSlider.value);
    }
}
