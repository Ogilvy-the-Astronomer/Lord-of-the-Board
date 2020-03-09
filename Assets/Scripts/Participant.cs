using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Participant : MonoBehaviour {
    [field: SerializeField]
    public bool IsPlayer { get; private set; }

    [SerializeField]
    protected Field field;

    public List<Card> deck;
    public List<Card> Hand;
    public List<Unit> ownUnits;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }


    public virtual void StartTurn() {
        //print("Start player turn");
        ownUnits.Clear();
        Unit[] unitsArray = FindObjectsOfType<Unit>();
        for (int i = 0; i < unitsArray.Length; i++) {
            if (unitsArray[i].playerOwned == IsPlayer && unitsArray[i].onField) {
                ownUnits.Add(unitsArray[i]);
                unitsArray[i].moveCount = 0;
            }
        }
    }

    public virtual void EndTurn() {

    }

    public virtual void Draw() {
        if (deck.Count > 0) {
            GameObject drawnCard = deck[0].gameObject;
            Hand.Add(deck[0]);
            deck.RemoveAt(0);
        }
        OrganizeHand();
    }

    public void OrganizeHand() {
        float space = 7f / Hand.Count;
        for (int i = 0; i < Hand.Count; i++) {
            Hand[i].transform.localPosition = new Vector3(3f - (Hand.Count * 0.3f) + (space * i), 0, 0);
        }
    }
}
