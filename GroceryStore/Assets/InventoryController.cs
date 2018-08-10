using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    private List<Item> inventoryList = new List<Item>();

    private UIHandler handlerUI;

	// Use this for initialization
	void Start ()
    {
        handlerUI = GetComponent<UIHandler>();
	}
	


    public void AddItem(Item inItem)
    {
        // if the inventory is empty
        if(inventoryList.Count < 3)
        {
            //add item to the first slot
            inventoryList.Add(inItem);

            int lastIndex = inventoryList.Count - 1;

            for(int i = lastIndex; i > 0; i--)
            {
                inventoryList[i] = inventoryList[i - 1];
            }

            inventoryList[0] = inItem;
            
        }
        // if there is at least one item in the inventory
        else
        {
            // shuffle items up one slot - discarding the last item
            inventoryList[2] = inventoryList[1];
            inventoryList[1] = inventoryList[0];
            inventoryList[0] = inItem;
        }

        handlerUI.UpdateInventoryUI(inventoryList);
    }

    public void RemoveItemByName(string inName)
    {
        for(int i=0; i < inventoryList.Count; i++)
        {
            inName = inName.ToUpper();

            if(inName.Equals(inventoryList[i].GetItemName().ToUpper()))
            {
                inventoryList.RemoveAt(i);
            }
        }

        handlerUI.UpdateInventoryUI(inventoryList);
    }



    public void RemoveItem(Item inItem)
    {
        for (int i = 0; i < inventoryList.Count; i++)
        {    
            if (inItem.GetItemName().Equals(inventoryList[i].GetItemName()))
            {
                inventoryList.RemoveAt(i);
            }
        }

        handlerUI.UpdateInventoryUI(inventoryList);
    }



    public void RemoveLastItem()
    {
        inventoryList.RemoveAt(inventoryList.Count - 1);

        handlerUI.UpdateInventoryUI(inventoryList);
    }

    public void ClearInventory()
    {
        inventoryList.Clear();

        handlerUI.UpdateInventoryUI(inventoryList);
    }


    public Item[] GetInventoryArray()
    {
        return inventoryList.ToArray();
    }


}
