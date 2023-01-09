using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{



    public GameObject dungeonRoom;
    public GameObject door;

    public AudioSource doorSource;

    public UI_Script uiscript;
    public bool gotDoorKey = false;




    // Start is called before the first frame update
    void Start()
    {
        doorSource = gameObject.GetComponent<AudioSource>();
        uiscript = FindObjectOfType<UI_Script>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            dungeonRoom.SetActive(true);
            Destroy(other);
            Destroy(this);

        }
    }



    public void OpenDoor()
    {
        if (gotDoorKey)
        {
            doorSource.Play();
            dungeonRoom.SetActive(true);
            door.SetActive(false);


        }



    }



    public void CallUIToggle()
    {
        uiscript.ToggleText1();
        uiscript.UnlockDoor();
    }



  




}
