using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class InteractionManager : MonoBehaviour // https://learn.unity.com/tutorial/placing-and-manipulating-objects-in-ar
{

    [SerializeField]
    ARRaycastManager m_RaycastManager;
    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
    [SerializeField]
    GameObject spawnablePrefab;

    Camera arCam;
    GameObject spawnedObject;
    public TMP_Text debugText;



    public UI_Script uiScript;

    // Start is called before the first frame update
    void Start()
    {
        spawnedObject = null;
        arCam = GameObject.Find("AR Camera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {

            if (Input.touchCount == 0)
                return;


        Ray ray = arCam.ScreenPointToRay(Input.GetTouch(0).position);

        if (m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits))
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began && spawnedObject == null)
            {
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    
                    spawnedObject = hit.collider.gameObject;

                    if (hit.collider.gameObject.CompareTag("Interactable"))//door
                    {

                        uiScript.ToggleText1();
                    }
                    else if (hit.collider.gameObject.CompareTag("Lockbox"))
                    {

                        uiScript.ToggleLockboxText();
                    }
                    else if (hit.collider.gameObject.CompareTag("Ring"))
                    {

                        uiScript.ToggleRingText();
                    }
                    else if (hit.collider.gameObject.CompareTag("Necklace"))
                    {

                        //uiScript.ToggleText1();
                    }

                    /*else
                    {
                        SpawnPrefab(m_Hits[0].pose.position);
                    }*/
                }

            }
            else if(Input.GetTouch(0).phase == TouchPhase.Moved && spawnedObject != null)
            {
                //if (spawnedObject.tag != "Interactable")
                //{
                    spawnedObject.transform.position = m_Hits[0].pose.position;
                //}

            }
            if(Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                    spawnedObject = null;
            }
        }

        debugText.text = spawnedObject.tag; 
        
    }


private void SpawnPrefab(Vector3 spawnPosition)
{
    spawnedObject = Instantiate(spawnablePrefab, spawnPosition, Quaternion.identity);
}




}
