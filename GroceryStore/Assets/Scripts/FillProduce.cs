using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillProduce : MonoBehaviour
{
    [SerializeField]
    private GameObject producePrefab;

	// Use this for initialization
	void Start ()
    {
		for(int i=0; i < transform.childCount; i++)
        {
            Instantiate(producePrefab, transform.GetChild(i));
        }
	}
}
