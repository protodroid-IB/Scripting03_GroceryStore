using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script Name: FillProduce.cs
// Written By: Laurence Valentini

public class FillProduce : MonoBehaviour
{
    // the prefab of stock to fill 
    [SerializeField]
    private GameObject producePrefab;

	// Use this for initialization
	void Start ()
    {
        // go through each child of this gameobject and create the prefab at the childs transform
		for(int i=0; i < transform.childCount; i++)
        {
            Instantiate(producePrefab, transform.GetChild(i));
        }
	}
}
