using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script Name: UIHandler.cs
// Written By: Laurence Valentini

public class UIHandler : MonoBehaviour
{

    private GameObject itemInfoUI; // the gameobject that holds all of the text gameobjects for item info UI
    private GameObject grabbedItemBoxesUI; //the item box images UI
    private GameObject objectiveUI; // the objective UI
    private GameObject keycodeUI; // the keycode UI

    private GameController gameController; 
    

    private Text nameUI, interactTypeUI, objective; // text components for item info's name interact type and the objective text
    private Text[] keycodeDigits; // keyocde text components for each digit

    private List<Image> inventoryUI = new List<Image>(); // list of inventory images

    private Color transparentColor = new Color(1f, 1f, 1f, 0f); // a colour that is set to transparent
    private Color solidColor = new Color(1f, 1f, 1f, 1f); // a colour that is completely visible

    // Use this for initialization
    void Start ()
    {
        // grab references
        gameController = GetComponent<GameController>();
        itemInfoUI = GameObject.FindWithTag("ItemInfoUI");
        grabbedItemBoxesUI = GameObject.FindWithTag("GrabbedItemBoxesUI");
        objectiveUI = GameObject.FindWithTag("ObjectiveUI");
        
        // grab all of the text components
        nameUI = itemInfoUI.transform.GetChild(0).GetComponent<Text>();
        interactTypeUI = itemInfoUI.transform.GetChild(1).GetComponent<Text>();
        objective = objectiveUI.transform.GetChild(2).GetComponent<Text>();

        // cycle through each child in the grabbed item boxes UI, grab the image and assign to the iventory UI image list
        int i = 0;
        foreach(Transform child in grabbedItemBoxesUI.transform)
        {
            inventoryUI.Add(child.GetChild(0).GetComponent<Image>());
            inventoryUI[i].color = transparentColor;
            i++;
        }

        // grab keycode text component references
        UpdateKeycodeReferences();

        // make sure item info is not visible
        itemInfoUI.SetActive(false);

        // make sure keycode ui is not visible
        keycodeUI.SetActive(false);
    }
	

    // when this method is called it changes the properties of the item info UI to show to the player
    public void DisplayItemInfo(string inName, string inType, string inInteractionType)
    {
        nameUI.text = inName.ToUpper(); 
        interactTypeUI.text = "LEFT MOUSE CLICK TO " + inInteractionType.ToUpper();
    }



    // when this method is called it updates the inventory UI
    public void UpdateInventoryUI(List<Item> inventory)
    {
        // cycle through each item in the passed in inventory
        for(int i=0; i < inventory.Count; i++)
        {
            // set the corresponding UI inventory image color to visible
            inventoryUI[i].color = solidColor;

            // set the corresponding UI inventory image sprite to the passed in inventory's
            inventoryUI[i].sprite = inventory[i].GetSprite();
        }

        // cycle through each remaining inventory UI and set its image to transparent
        for(int i = inventory.Count; i < inventoryUI.Count; i++)
        {
            inventoryUI[i].color = transparentColor;
        }
    }


    // this method updates the objective UI
    public void UpdateObjective(string newObjective)
    {
        // if the objective state is start or finish
        if(gameController.GetObjectiveState() == ObjectiveState.Start || gameController.GetObjectiveState() == ObjectiveState.Finish)
        {
            // if the objective UI is active set to false
            if(objectiveUI.activeSelf == true) objectiveUI.SetActive(false);
        }
        
        // if any other objective state
        else
        {
            // if the objective UI is not active make it active
            if(objectiveUI.activeSelf == false) objectiveUI.SetActive(true);

            // set the text to the new objetive
            objective.text = newObjective.ToUpper();
        }

        // if the objective state is find key or key found
        if(gameController.GetObjectiveState() == ObjectiveState.FindKey || gameController.GetObjectiveState() == ObjectiveState.KeyFound)
        {
            //if the keycode UI is not active make it active
            if (keycodeUI.activeSelf == false) keycodeUI.SetActive(true);
        }
        else
        {
            // if the keycode UI is active make it not active
            if (keycodeUI.activeSelf == true) keycodeUI.SetActive(false);
        }
    }


    // this method updates the keycode digits
    public void UpdateKeyCodeDigits(int[] inDigits)
    {
        // grabs the refrences to the text components
        UpdateKeycodeReferences();

        // cycles through each digit in the integer array passed in
        for (int i=0; i < inDigits.Length; i++)
        {
            // if it equals -1 that digit hasn't been found yet so set it to a '?'
            if (inDigits[i] == -1) keycodeDigits[i].text = "?";

            // otherwise it has been found and set that digit to the found digit
            else keycodeDigits[i].text = inDigits[i].ToString();
        }
    }


    // this method grabs the keycode text components and sets them
    private void UpdateKeycodeReferences()
    {
        keycodeUI = GameObject.FindWithTag("KeycodeUI");

        keycodeDigits = new Text[keycodeUI.transform.childCount-1];

        for (int j = 0; j < keycodeDigits.Length; j++)
        {
            keycodeDigits[j] = keycodeUI.transform.GetChild(j+1).GetComponent<Text>();
        }
    }



    // this method updates the objective title and description
    public void UpdateObjectiveDialogue(string inTitle, string inObjective)
    {
        GameObject.FindWithTag("ObjectiveDialogue").transform.GetChild(2).GetComponent<Text>().text = "\n" + inObjective.ToUpper();
        GameObject.FindWithTag("ObjectiveDialogue").transform.GetChild(1).GetComponent<Text>().text = inTitle.ToUpper();
    }
}
