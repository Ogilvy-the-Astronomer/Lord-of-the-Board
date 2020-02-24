using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {
    public Card occupier;
    public bool spawnPoint;
    public Vector2 position;

    Player player;
	// Use this for initialization
	void Start () {
        player = Camera.main.gameObject.GetComponent<Player>();
        //position = new Vector2(int.Parse(transform.name[0].ToString()), int.Parse(transform.name[1].ToString()));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown() {
        if (spawnPoint && player.heldCard) {
            
        }
    }
}
