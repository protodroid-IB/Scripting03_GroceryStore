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


    private GameObject itemInfoUI;


	// Use this for initialization
	void Awake ()
    {
        mainCam = Camera.main;
        itemInfoUI = GameObject.FindWithTag("ItemInfoUI");
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
                itemInfoUI.SetActive(true);

                interactable.Hover();

                if(Input.GetMouseButtonDown(0))
                {
                    interactable.Interact();
                }
            }
            else
            {
                itemInfoUI.SetActive(false);
            }
        }
        else
        {
            rayDirectionPoint = rayOrigin + (mainCam.transform.forward * interactRange);
            gameObjectHit = emptyGO;
            itemInfoUI.SetActive(false);
        }


        if (debugMode == true)
        {
            Debug.Log("HIT: " + gameObjectHit);
        }
    }



    public GameObject GetGameObjectHit()
    {
        return gameObjectHit;
    }
}
