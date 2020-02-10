using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {
    public GameObject[,] tiles = new GameObject[7,7];
	// Use this for initialization
	void Start () {
        for (int i = 0; i < 7; i++) {
            for(int j = 0; j < 7; j++){
                tiles[i, j] = transform.GetChild(j * 7 + i).gameObject;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {

	}
}
