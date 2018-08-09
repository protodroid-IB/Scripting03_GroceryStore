using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    private GameObject itemInfoUI;
    private GameObject grabbedItemBoxesUI;
    private GameObject objectiveUI;
    private GameController gameController;

    private Text nameUI, interactTypeUI, objective;

    private List<Image> inventoryUI = new List<Image>();

    private Color transparentColor = new Color(1f, 1f, 1f, 0f);
    private Color solidColor = new Color(1f, 1f, 1f, 1f);

    // Use this for initialization
    void Start ()
    {
        gameController = GetComponent<GameController>();

        itemInfoUI = GameObject.FindWithTag("ItemInfoUI");
        grabbedItemBoxesUI = GameObject.FindWithTag("GrabbedItemBoxesUI");
        objectiveUI = GameObject.FindWithTag("ObjectiveUI");

        nameUI = itemInfoUI.transform.GetChild(0).GetComponent<Text>();
        interactTypeUI = itemInfoUI.transform.GetChild(1).GetComponent<Text>();

        objective = objectiveUI.transform.GetChild(2).GetComponent<Text>();

        int i = 0;
        foreach(Transform child in grabbedItemBoxesUI.transform)
        {
            inventoryUI.Add(child.GetChild(0).GetComponent<Image>());
            inventoryUI[i].color = transparentColor;
            i++;
        }

        itemInfoUI.SetActive(false);
    }
	

    public void DisplayItemInfo(string inName, string inType, string inInteractionType)
    {
        nameUI.text = inName.ToUpper();
        interactTypeUI.text = "LEFT MOUSE CLICK TO " + inInteractionType.ToUpper();
    }




    public void UpdateInventoryUI(List<Item> inventory)
    {
        for(int i=0; i < inventory.Count; i++)
        {
            inventoryUI[i].color = solidColor;
            inventoryUI[i].sprite = inventory[i].GetSprite();
        }

        for(int i = inventory.Count; i < inventoryUI.Count; i++)
        {
            inventoryUI[i].color = transparentColor;
        }
    }



    public void UpdateObjective(string newObjective)
    {
        if(gameController.GetObjectiveState() == ObjectiveState.Start || gameController.GetObjectiveState() == ObjectiveState.Finish)
        {
            if(objectiveUI.activeSelf == true) objectiveUI.SetActive(false);
        }
        else
        {
            if(objectiveUI.activeSelf == false) objectiveUI.SetActive(true);

            objective.text = newObjective.ToUpper();
        }
        
    }
}
