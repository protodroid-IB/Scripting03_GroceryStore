using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    private GameObject itemInfoUI;

    private Text nameUI, typeUI, interactTypeUI; 

    // Use this for initialization
    void Start ()
    {
        itemInfoUI = GameObject.FindWithTag("ItemInfoUI");

        nameUI = itemInfoUI.transform.GetChild(0).GetComponent<Text>();
        typeUI = itemInfoUI.transform.GetChild(1).GetComponent<Text>();
        interactTypeUI = itemInfoUI.transform.GetChild(2).GetComponent<Text>();

        itemInfoUI.SetActive(false);
    }
	

    public void DisplayItemInfo(string inName, string inType, string inInteractionType)
    {
        nameUI.text = inName.ToUpper();
        typeUI.text = inType.ToUpper();
        interactTypeUI.text = inInteractionType.ToUpper();
    }
}
