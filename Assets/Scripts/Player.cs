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

    public List<Unit> units;
	// Use this for initialization
	void Start () {
        field = Object.FindObjectOfType<Field>();
	}
	
	// Update is called once per frame
	void Update () {
        if (heldCard) {
            heldCard.transform.position = transform.position + GetComponent<Camera>().ScreenPointToRay(Input.mousePosition).direction * 3.0f;
            if (Input.GetMouseButtonDown(0)) {
                Play();
            }
            else if (Input.GetMouseButtonDown(1)) {
                heldCard = null;
                OrganizeHand();
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
                            currentObject = null;
                            OrganizeHand();
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

    public void StartTurn() {
        //print("Start player turn");
        units.Clear();
        Unit[] unitsArray = FindObjectsOfType<Unit>();
        for (int i = 0; i < unitsArray.Length; i++) {
            if (unitsArray[i].playerOwned && unitsArray[i].onField) {
                units.Add(unitsArray[i]);
                unitsArray[i].moveCount = 0;
            }
        }
    }

    public void EndTurn() {

    }

    void Play() {
        if (heldCard.GetComponent<Unit>()) {
            RaycastHit hit;
            LayerMask mask = ~(1 << LayerMask.NameToLayer("Card"));
            if (Physics.Raycast(GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, 100.0f, mask)) {
                TileScript hitTile = hit.transform.GetComponent<TileScript>();
                if (hitTile) {
                    if (heldCard.GetComponent<Unit>().IsStructure) {
                        //do this better >:(
                        Unit[] unitsArray = FindObjectsOfType<Unit>();
                        for (int i = 0; i < unitsArray.Length; i++) {
                            if (unitsArray[i].playerOwned && Vector2.Distance(hitTile.position, unitsArray[i].position) == 1) {
                                for (int j = 0; j < Hand.Count; j++) {
                                    if (Hand[j] == heldCard.GetComponent<Card>()) {
                                        units.Add(Hand[j].GetComponent<Unit>());
                                        Hand.RemoveAt(j);                                        
                                        j = Hand.Count + 1;
                                    }
                                }
                                heldCard.GetComponent<Card>().Play(hitTile.position);
                                heldCard = null;
                                i = unitsArray.Length + 1;
                            }
                        }
                        
                    }
                    else {
                        if (hitTile.spawnPoint) {
                            for (int i = 0; i < Hand.Count; i++) {
                                if (Hand[i] == heldCard.GetComponent<Card>()) {
                                    units.Add(Hand[i].GetComponent<Unit>());
                                    Hand.RemoveAt(i);
                                    i = Hand.Count + 1;
                                }
                            }
                            heldCard.GetComponent<Card>().Play(hitTile.position);
                            heldCard = null;
                        }
                    }
                }
            }
        }
        else if (heldCard.GetComponent<Spell>()) {
            if (heldCard.GetComponent<Spell>().AcivateConditions) {
                heldCard.GetComponent<Spell>().Play();
                for (int i = 0; i < Hand.Count; i++) {
                    if (Hand[i] == heldCard.GetComponent<Card>()) {
                        Hand.RemoveAt(i);
                        i = Hand.Count + 1;
                    }
                }
                heldCard.transform.Translate(0, 500, 0);
                heldCard = null;
            }
        }
        else if (heldCard.GetComponent<Enhancement>()) {
            RaycastHit hit;
            LayerMask mask = (1 << LayerMask.NameToLayer("Card"));
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            ray.origin += ray.direction * 3.1f;
            if (Physics.Raycast(ray, out hit, 100.0f, mask)) {
                Unit hitUnit = hit.transform.GetComponent<Unit>();
                print(hit.transform.name);
                if (hitUnit) {
                    if (!hitUnit.enhancement) {
                        for (int i = 0; i < Hand.Count; i++) {
                            if (Hand[i] == heldCard.GetComponent<Card>()) {
                                Hand.RemoveAt(i);
                                i = Hand.Count + 1;
                            }
                        }
                        heldCard.GetComponent<Card>().Play(hitUnit.position);
                        heldCard = null;
                    }
                }
            }
        }
    }

    public void Draw() {
        if (deck.Count > 0) {
            GameObject drawnCard = deck[0].gameObject;
            Hand.Add(deck[0]);
            deck.RemoveAt(0);
            OrganizeHand();
        }
    }

    public void OrganizeHand() {
        float space = 7f / Hand.Count;
        for (int i = 0; i < Hand.Count; i++) {
            Hand[i].transform.localPosition = new Vector3(3f - (Hand.Count * 0.3f) + (space * i), 0, 0);
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
