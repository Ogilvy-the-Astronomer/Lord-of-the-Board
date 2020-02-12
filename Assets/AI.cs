using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    [SerializeField]
    Field field;

    [SerializeField]
    List<Unit> playerUnits;

    public List<GameObject> Hand;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Turn() {
        Unit target = null;
        float targetThreat = -1;
        Unit[] units = Object.FindObjectsOfType<Unit>();
        print(units.Length);
        for (int i = 0; i < units.Length; i++) {
            if (units[i].playerOwned) {
                playerUnits.Add(units[i]);
            }
        }
        for (int i = 0; i < playerUnits.Count; i++) {
            if (playerUnits[i].threat > targetThreat) {
                target = playerUnits[i];
                targetThreat = target.threat;
            }
        }
        int column = (int)target.position.x;
        int cardToSummon = 0;
        float highestValue = -1;
        for (int i = 0; i < Hand.Count; i++) {
            if (Hand[i].GetComponent<Unit>().value > highestValue) {
                cardToSummon = i;
                highestValue = Hand[i].GetComponent<Unit>().value;
            }
        }
        Summon(cardToSummon, column);
        //field.tiles[column, 0].transform.position += new Vector3(0,1,0);

    }

    void Summon(int handIndex, int column) {
        Hand[handIndex].GetComponent<Card>().playerOwned = false;
        Hand[handIndex].GetComponent<Card>().Summon(new Vector3(column, 0));
        Hand.RemoveAt(handIndex);
    }
}
