using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Script : MonoBehaviour
{

    public TMP_Text clueText;
    private float clueTimer = 5;
    private bool showClueText = false;

    public GameObject gpsUI;

    public GameObject text1;
    public GameObject text2_lockbox;
    public GameObject text3_ring;
    public GameObject text4_letter1;
    public GameObject text5_nextclue;
    public GameObject text6_Necklace;
    public GameObject text7_Key;
    public GameObject text8_Axe;
    public GameObject text9_Intro;


    bool showtext1 = true;
    bool showtext4 = true;
    bool showtext5 = true;
    bool showtext6 = true;
    bool showtext7 = true;
    bool showtext8 = true;

    bool gotKey = false;

    DoorScript doorScript;
    AudioSource source;
    public AudioClip paperSound;
    public AudioClip lockboxSound;


    // Start is called before the first frame update
    void Start()
    {
        doorScript = FindObjectOfType<DoorScript>();
        source = gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        if (showClueText)
        {
            clueTimer -= Time.deltaTime;
            if (clueTimer <= 0)
            {
                showClueText = false;
                clueTimer = 3;
                clueText.gameObject.SetActive(false);
                clueText.text = "Search for the next clue in this area!"; 
            }
        }
    }

    public void ShowClueText()
    {
        clueText.gameObject.SetActive(true);
        showClueText = true;
        clueTimer = 3;
    }






    public void ToggleGPSUI()
    {
        gpsUI.SetActive(!gpsUI.activeSelf);
    }


    public void ToggleText1()
    {
        if (showtext1)
        {
            text1.SetActive(!text1.activeSelf);
            showtext1 = false;

        }
        else
        {
            text1.SetActive(false);
        }


    }

    public void ToggleLockboxText()
    {
        text2_lockbox.SetActive(!text2_lockbox.activeSelf);
    }


    public void ToggleRingText()
    {
        text3_ring.SetActive(!text3_ring.activeSelf);
    }

    public void ToggleLetter1Text()
    {
        if (showtext4)
        {
            source.PlayOneShot(lockboxSound);
            text4_letter1.SetActive(!text4_letter1.activeSelf);
            showtext4 = false;
        }
        else
        {
            text4_letter1.SetActive(false);
        }


    }

    public void ToggleClue1()
    {
        if (showtext5)
        {
            source.PlayOneShot(paperSound);
            text5_nextclue.SetActive(!text5_nextclue.activeSelf);
            showtext5 = false; 
        }
        else
        {
            source.PlayOneShot(paperSound);
            text5_nextclue.SetActive(false);
        }

    }


    public void ToggleNecklaceText()
    {
        if (showtext6)
        {
            text6_Necklace.SetActive(!text6_Necklace.activeSelf);
            showtext6 = false;

        }
        else
        {
            text6_Necklace.SetActive(false);
        }
    }


    public void ToggleKeyText()
    {
        if (showtext7)
        {
            text7_Key.SetActive(!text7_Key.activeSelf);
            gotKey = true;
            showtext7 = false;
            doorScript = FindObjectOfType<DoorScript>();
            doorScript.gotDoorKey = true;

        }
        else
        {
            text7_Key.SetActive(false);
        }
    }


    public void ToggleAxeText()
    {
        if (showtext8)
        {
            text8_Axe.SetActive(!text8_Axe.activeSelf);
            showtext8 = false;

        }
        else
        {
            text8_Axe.SetActive(false);
        }
    }



    public void ToggleIntro()
    {
        text9_Intro.SetActive(!text9_Intro.activeSelf);
    }


    public void UnlockDoor()
    {
        if (gotKey)
        {
            doorScript.OpenDoor();
        }
    }


}
