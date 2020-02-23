using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    [SerializeField]
    Field field;

    [SerializeField]
    List<Unit> playerUnits;

    [SerializeField]
    List<Unit> ownUnits;

    public List<Card> Hand;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Turn() {
        playerUnits.Clear();
        ownUnits.Clear();
        Unit target = null;
        float targetThreat = -1000;
        Unit[] units = Object.FindObjectsOfType<Unit>();
        //print(units.Length);
        for (int i = 0; i < units.Length; i++) {
            if (units[i].onField) {
                if (units[i].playerOwned) {
                    playerUnits.Add(units[i]);
                }
                else {
                    ownUnits.Add(units[i]);
                }
            }
        }
        for (int i = 0; i < playerUnits.Count; i++) {
            playerUnits[i].threat = playerUnits[i].value * Mathf.Pow((7 - playerUnits[i].position.y), 2);
            float dist = 0;
            for (int j = 0; j < ownUnits.Count; j++) {
                dist = Mathf.Pow(10 - Vector2.Distance(playerUnits[i].position, ownUnits[j].position), 1.15f);
                playerUnits[i].threat -= dist; //plus ownUnits[j].value modified by some value
            }
            //print(playerUnits[i].gameObject.name + " is " + dist + " units away");

            if (playerUnits[i].threat > targetThreat) {
                target = playerUnits[i];
                targetThreat = target.threat;
            }
        }
        int column = 3;
        if(target) column = (int)target.position.x;
        int cardToSummon = 0;
        float highestValue = -1;
        if (Hand.Count > 0) {
            for (int i = 0; i < Hand.Count; i++) {
                if (Hand[i].GetComponent<Unit>().value > highestValue) {
                    cardToSummon = i;
                    highestValue = Hand[i].GetComponent<Unit>().value;
                }
            }

            Summon(cardToSummon, column);
        }
        //field.tiles[column, 0].transform.position += new Vector3(0,1,0);

    }

    bool Summon(int handIndex, int column) {
        if (!field.tiles[column, 0].GetComponent<TileScript>().occupier) {
            Hand[handIndex].playerOwned = false;
            Hand[handIndex].Summon(new Vector3(column, 0));
            Hand.RemoveAt(handIndex);
            return true;
        }
        else {
            int upper;
            int lower;
            for (int i = 0; i < 6; i++) {
                upper = column + i;
                lower = column - i;
                if (upper > 6) upper = 6;
                if (lower < 0) lower = 0;
                if (!field.tiles[upper, 0].GetComponent<TileScript>().occupier) {
                    Hand[handIndex].playerOwned = false;
                    Hand[handIndex].Summon(new Vector3(upper, 0));
                    Hand.RemoveAt(handIndex);

                    i = 7;
                    return true;
                }
                else if (!field.tiles[lower, 0].GetComponent<TileScript>().occupier) {
                    Hand[handIndex].playerOwned = false;
                    Hand[handIndex].Summon(new Vector3(lower, 0));
                    Hand.RemoveAt(handIndex);

                    i = 7;
                    return true;
                }
            }
        }
        return false;
    }
}
