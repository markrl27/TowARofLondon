using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBoxScript : MonoBehaviour
{


    public UI_Script uiscript;



    // Start is called before the first frame update
    void Start()
    {

        uiscript = FindObjectOfType<UI_Script>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void CallLockboxUI()
    {
        uiscript.ToggleLockboxText();
    }

    public void CallRingUI()
    {
        uiscript.ToggleRingText();
    }

    public void CallNecklaceUI()
    {
        uiscript.ToggleNecklaceText();
    }    
    
    public void CallKeyUI()
    {
        uiscript.ToggleKeyText();
    }
    
    
    
    public void CallAxeUI()
    {
        uiscript.ToggleAxeText();
    }




}
