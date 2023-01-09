using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetNavigationTarget : MonoBehaviour // adapted from tutorial: https://www.youtube.com/watch?v=fuHFrMZ4q_s&ab_channel=FireDragonGameStudio 
{
    [SerializeField]
    private Camera topDownCam;
    [SerializeField]
    private GameObject navTarget;

    private NavMeshPath path;
    private LineRenderer line;

    private bool lineToggle;


    // Start is called before the first frame update
    void Start()
    {
        path = new NavMeshPath();
        line = transform.GetComponent<LineRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            lineToggle = !lineToggle;
        }

        if (lineToggle)
        {
            NavMesh.CalculatePath(transform.position, navTarget.transform.position, NavMesh.AllAreas, path);
            line.positionCount = path.corners.Length;
            line.SetPositions(path.corners);
            line.enabled = true;
        }
        else
        {
            line.enabled = false;
        }
        
    }
}
