using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {
    public GameObject[,] tiles = new GameObject[7,7];
    public List<Card> cards;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < 7; i++) {
            for(int j = 0; j < 7; j++){
                tiles[i, j] = transform.GetChild(j * 7 + i).gameObject;
                tiles[i, j].GetComponent<TileScript>().position = new Vector2(i, j);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {

	}
}
