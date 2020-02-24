using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    GameObject currentObject;

    Field field;

    public List<Card> deck;
    public List<Card> hand;
    public GameObject heldCard;
	// Use this for initialization
	void Start () {
        field = Object.FindObjectOfType<Field>();
	}
	
	// Update is called once per frame
	void Update () {
        if (heldCard) {
            heldCard.transform.position = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0)) {
                Summon();
            }
        }

        if (Input.GetMouseButtonDown(0) && !heldCard) {
            RaycastHit hit;
            if (Physics.Raycast(GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit)) {
                Card hitCard = hit.transform.GetComponent<Card>();
                if (hitCard) {
                    if (hitCard.playerOwned) {
                        if (hitCard.onField) {
                            currentObject = hit.transform.gameObject;
                        }
                        else {
                            heldCard = hit.transform.gameObject;
                        }
                    }
                }
            }
        }
    }

    void Summon() {
        RaycastHit hit;
        LayerMask mask = ~(LayerMask.NameToLayer("Card"));
        if (Physics.Raycast(GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit)) {
            TileScript hitTile = hit.transform.GetComponent<TileScript>();
            if (hitTile) {
                if (hitTile.spawnPoint) {
                    for (int i = 0; i < hand.Count; i++) {
                        if (hand[i] == heldCard.GetComponent<Card>()) {
                            hand.RemoveAt(i);
                            i = hand.Count + 1;
                        }
                    }
                    heldCard.GetComponent<Card>().Summon(hitTile.position);
                    heldCard = null;
                }
            }
        }
    }

    public void Forward() {
        if (currentObject) {
            currentObject.GetComponent<Unit>().Forward();
        }
    }
    public void Back() {
        if (currentObject) {
            currentObject.GetComponent<Unit>().Back();
        }
    }
    public void Right() {
        if (currentObject) {
            currentObject.GetComponent<Unit>().Right();
        }
    }
    public void Left() {
        if (currentObject) {
            currentObject.GetComponent<Unit>().Left();
        }
    }
}
