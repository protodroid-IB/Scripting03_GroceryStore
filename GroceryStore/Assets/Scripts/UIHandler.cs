using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    private GameObject itemInfoUI;
    private GameObject grabbedItemBoxesUI;
    private GameObject objectiveUI;
    private GameObject keycodeUI;

    private GameController gameController;
    

    private Text nameUI, interactTypeUI, objective;
    private Text[] keycodeDigits;

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

        UpdateKeycodeReferences();

        itemInfoUI.SetActive(false);
        keycodeUI.SetActive(false);
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

        if(gameController.GetObjectiveState() == ObjectiveState.FindKey || gameController.GetObjectiveState() == ObjectiveState.KeyFound)
        {
            if (keycodeUI.activeSelf == false) keycodeUI.SetActive(true);
        }
        else
        {
            if (keycodeUI.activeSelf == true) keycodeUI.SetActive(false);
        }
    }


    public void UpdateKeyCodeDigits(int[] inDigits)
    {
        UpdateKeycodeReferences();

        for (int i=0; i < inDigits.Length; i++)
        {
            if (inDigits[i] == -1) keycodeDigits[i].text = "?";
            else keycodeDigits[i].text = inDigits[i].ToString();
        }
    }


    private void UpdateKeycodeReferences()
    {
        keycodeUI = GameObject.FindWithTag("KeycodeUI");

        keycodeDigits = new Text[keycodeUI.transform.childCount-1];

        for (int j = 0; j < keycodeDigits.Length; j++)
        {
            keycodeDigits[j] = keycodeUI.transform.GetChild(j+1).GetComponent<Text>();
        }
    }



    public void UpdateObjectiveDialogue(string inObjective)
    {

        GameObject.FindWithTag("ObjectiveDialogue").transform.GetChild(2).GetComponent<Text>().text = inObjective.ToUpper();
    }
}
