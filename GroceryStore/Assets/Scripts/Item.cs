﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Item : MonoBehaviour
{
    [SerializeField]
    private string itemName = "NOT NAMED";

    [SerializeField]
    private Sprite sprite = null;

    [SerializeField]
    private ItemType itemType = ItemType.None;

    [SerializeField]
    private bool canInteract = true;

    [SerializeField]
    private string interactType = "collect";

    private SpriteRenderer thisSR;
    private Interactable interactable;

    private void Start()
    {
        thisSR = GetComponent<SpriteRenderer>();
        thisSR.sprite = sprite;

        interactable = GetComponent<Interactable>();

        if(canInteract == true)
        {
            interactable.enabled = true;
        }
        else
        {
            interactable.enabled = false;
        }
    }

    public string GetItemName()
    {
        return itemName;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public ItemType GetItemType()
    {
        return itemType;
    }

    public bool GetInteract()
    {
        return canInteract;
    }

    public void SetInteract(bool inBool)
    {
        canInteract = inBool;
        interactable.enabled = inBool;
    }

    public string GetInteractType()
    {
        return interactType;
    }
}