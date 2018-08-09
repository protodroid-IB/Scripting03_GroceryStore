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
    private InventoryController inventory;


	// Use this for initialization
	void Awake ()
    {
        mainCam = Camera.main;
        itemInfoUI = GameObject.FindWithTag("ItemInfoUI");
        inventory = GameObject.FindWithTag("GameController").GetComponent<InventoryController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        LookAt();

        if(Input.GetKeyDown(KeyCode.C))
        {
            inventory.ClearInventory();
        }
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
                    Item newItem = interactable.GetInteractedItem();
                    inventory.AddItem(newItem);
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
