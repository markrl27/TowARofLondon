using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeScript : MonoBehaviour
{


    public TMP_InputField inputField;
    public UI_Script uiScript;
    //public GameObject lockbox;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(inputField.text == "1533")
        {
            uiScript.ToggleLockboxText();
            uiScript.ToggleLetter1Text();
            //Destroy(lockbox); 
        }

        
    }
}
