using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Item))]
public class Interactable : MonoBehaviour
{
    private Item thisItem;
    private UIHandler handlerUI;

    private bool canInteract = true;


	// Use this for initialization
	void Start ()
    {
        thisItem = GetComponent<Item>();
        handlerUI = GameObject.FindWithTag("GameController").transform.GetComponent<UIHandler>();
        canInteract = thisItem.GetInteract();
    }
	
	// Update is called once per frame
	void Update () {
		
	}



    public bool Hover()
    {
        // display UI data
        handlerUI.DisplayItemInfo(thisItem.GetItemName(), thisItem.GetItemType().ToString(), thisItem.GetInteractType());

        // item glow

        return true;
    }

    public Item GetInteractedItem()
    {
        Debug.Log("INTERACTED!");

        return thisItem;
    }


    public void SetInteract(bool inBool)
    {
        canInteract = inBool;
    }

    public bool GetInteractable()
    {
        return canInteract;
    }
}
