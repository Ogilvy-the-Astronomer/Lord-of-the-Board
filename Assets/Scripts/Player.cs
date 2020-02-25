using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    GameObject currentObject;

    Field field;

    public List<Card> deck;
    public List<Card> Hand;
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
                    else {
                        if (currentObject) {
                            Unit hitUnit = hitCard.GetComponent<Unit>();
                            if (hitUnit) {
                                print(hitUnit.transform.name);
                                if (hitUnit.onField && Vector2.Distance(currentObject.GetComponent<Unit>().position, hitUnit.position) <= 1.0f) {
                                    currentObject.GetComponent<Unit>().Attack(hitUnit);
                                }
                            }
                            //print("Attack!");
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
                    for (int i = 0; i < Hand.Count; i++) {
                        if (Hand[i] == heldCard.GetComponent<Card>()) {
                            Hand.RemoveAt(i);
                            i = Hand.Count + 1;
                        }
                    }
                    heldCard.GetComponent<Card>().Summon(hitTile.position);
                    heldCard = null;
                }
            }
        }
    }

    public void Draw() {
        if (deck.Count > 0) {
            GameObject drawnCard = deck[0].gameObject;
            Hand.Add(deck[0]);
            deck.RemoveAt(0);
            float space = 7f / Hand.Count;
            for (int i = 0; i < Hand.Count; i++) {
                Hand[i].transform.localPosition = new Vector3(3f - (Hand.Count * 0.3f) + (space * i), 0, 0);
            }
        }
    }

    public void Attack() {
        RaycastHit hit;
        if (currentObject) {
            if (Physics.Raycast(GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit)) {
                Unit hitCard = hit.transform.GetComponent<Unit>();
                if (hitCard) {
                    if (!hitCard.playerOwned) {
                        if (hitCard.onField && Vector2.Distance(currentObject.GetComponent<Unit>().position, hitCard.position) <= 1.0f) {
                            currentObject.GetComponent<Unit>().Attack(hitCard);
                        }
                    }
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
