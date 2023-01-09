using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavenScript : MonoBehaviour
{


    Animator ravenAnimator;
    AudioSource ravenSource;
    UI_Script uiscript;

    public GameObject key;




    // Start is called before the first frame update
    void Start()
    {
       // key.SetActive(false);

        ravenAnimator = gameObject.GetComponent<Animator>();
        ravenSource = gameObject.GetComponent<AudioSource>();
        uiscript = FindObjectOfType<UI_Script>();



    }





    // Update is called once per frame
    void Update()
    {

        if (ravenAnimator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            gameObject.SetActive(false);
        }


        
    }


    public void TapRaven() 
    {
        ravenAnimator.SetBool("FlyAway", true);
        ravenSource.Play();
        uiscript.clueText.text = "No luck, try again!";
        uiscript.ShowClueText();
    }

    public void ActivateKey()
    {
        uiscript.clueText.text = "You found the key!";

        key.SetActive(true);
    }


}
