using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {
    public Card occupier;
    public bool spawnPoint;
    public Vector2 position;
    public float threat;

    Player player;
	// Use this for initialization
	void Start () {
        player = Camera.main.gameObject.GetComponent<Player>();
        //position = new Vector2(int.Parse(transform.name[0].ToString()), int.Parse(transform.name[1].ToString()));
	}
	
	// Update is called once per frame
	void Update () {
        if (threat > 0) {
            GetComponent<Renderer>().material.color = new Color(threat, 0, 0, 0.3f);
        }
        else {
            GetComponent<Renderer>().material.color = new Color(0, 0, -threat, 0.3f);
        }
	}

    void OnMouseDown() {
        if (spawnPoint && player.heldCard) {
            
        }
    }
}
