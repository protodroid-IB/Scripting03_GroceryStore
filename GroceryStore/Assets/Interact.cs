using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interact : MonoBehaviour
{
    private Camera mainCam;

    [SerializeField]
    private float interactRange = 20f;

    private GameObject gameObjectHit;

    private GameObject emptyGO = null;

    private Vector3 rayDirectionPoint;

    [SerializeField]
    private bool debugMode = false;

   // private LineRenderer lineOfSight;

	// Use this for initialization
	void Start ()
    {
        mainCam = Camera.main;
       // lineOfSight = GetComponent<LineRenderer>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        LookAt();
	}



    public void LookAt()
    {
        Vector3 rayOrigin = mainCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));

        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, mainCam.transform.forward, out hit, interactRange))
        {
            rayDirectionPoint = hit.point;
            gameObjectHit = hit.transform.gameObject;

            Interactable interactable = gameObjectHit.GetComponent<Interactable>();

            if(interactable != null)
            {
                interactable.Hover();

                if(Input.GetMouseButtonDown(0))
                {
                    interactable.Interact();
                }
            }
        }
        else
        {
            rayDirectionPoint = rayOrigin + (mainCam.transform.forward * interactRange);
            gameObjectHit = emptyGO;
        }


        if (debugMode == true)
        {
            //if (lineOfSight.enabled == false) lineOfSight.enabled = true;

            //lineOfSight.SetPosition(0, rayOrigin);
            //lineOfSight.SetPosition(1, rayDirectionPoint);

            Debug.Log("HIT: " + gameObjectHit);
        }
        else
        {
            //if (lineOfSight == true) lineOfSight.enabled = false;
        }
    }



    public GameObject GetGameObjectHit()
    {
        return gameObjectHit;
    }
}
