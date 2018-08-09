using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    private Item thisItem;
    private UIHandler handlerUI;


	// Use this for initialization
	void Start ()
    {
        thisItem = GetComponent<Item>();
        handlerUI = GameObject.FindWithTag("GameController").transform.GetComponent<UIHandler>();
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

        if (thisItem.GetItemType() != ItemType.Device)
        {
            return thisItem;
        }
        else return null;
    }
}
