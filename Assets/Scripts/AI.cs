using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    [SerializeField]
    Field field;

    public Avatar avatar;
    public Avatar playerAvatar;

    public List<Unit> playerUnits;

    public List<Unit> ownUnits;

    public List<Card> deck;
    public List<Card> Hand;
    // Use this for initialization
    void Start () {
        Avatar[] avatars = FindObjectsOfType<Avatar>();
        if (avatars[0].playerOwned) {
            playerAvatar = avatars[0];
            avatar = avatars[1];
        }
        else {
            avatar = avatars[0];
            playerAvatar = avatars[1];
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartTurn() {
        playerUnits.Clear();
        ownUnits.Clear();
        Unit target = null;
        //float targetThreat = -1000;
        Unit[] units = Object.FindObjectsOfType<Unit>();
        //print(units.Length);
        Draw();
        for (int i = 0; i < units.Length; i++) {
            if (units[i].onField) {
                if (units[i].playerOwned) {
                    playerUnits.Add(units[i]);
                }
                else {
                    ownUnits.Add(units[i]);
                    units[i].moveCount = 0;
                }
            }
        }
        playerUnits = CalculateThreat();
        if(playerUnits.Count > 0) target = playerUnits[0];
        int column = (int)playerAvatar.position.x;
        if(target) column = (int)target.position.x;
        int cardToSummon = 0;
        float highestValue = -1;
        if (Hand.Count > 0) {
            for (int i = 0; i < Hand.Count; i++) {
                if (Hand[i].GetComponent<Unit>() && Hand[i].GetComponent<Unit>().value > highestValue) {
                    cardToSummon = i;
                    highestValue = Hand[i].GetComponent<Unit>().value;
                }
            }

            Summon(cardToSummon, column);
        }

        for (int i = 0; i < ownUnits.Count; i++) {
            Unit closestUnit = playerUnits[0];
            float closestDistance = 1000;
            float dist = 1000;
            for (int j = 0; j < playerUnits.Count; j++) {
                dist = Vector2.Distance(ownUnits[i].position, playerUnits[j].position);
                //print(dist);
                //if (dist <= 1) continue;
                if (dist < closestDistance) {
                    closestDistance = dist;
                    closestUnit = playerUnits[j];
                }
            }
            Vector2 vec = closestUnit.position - ownUnits[i].position;
            //print(closestUnit.transform.name);
            bool moved = false;
            if (vec.x > 0) {
                moved = ownUnits[i].Right();
            }
            else if (vec.x < 0) {
                moved = ownUnits[i].Left();
            }
            if (!moved) {
                if (vec.y > 0) {
                    moved = ownUnits[i].Back();
                }
                else if (vec.y < 0) {
                    moved = ownUnits[i].Forward();
                }
            }
            if (Vector2.Distance(ownUnits[i].position, closestUnit.position) <= 1.0f) {
                ownUnits[i].Attack(closestUnit);
            }
        }
        //field.tiles[column, 0].transform.position += new Vector3(0,1,0);
        FindObjectOfType<TurnController>().EndTurn();

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
    bool Summon(int handIndex, int column) {
        if (!field.tiles[column, 0].GetComponent<TileScript>().occupier) {
            Hand[handIndex].playerOwned = false;
            Hand[handIndex].Play(new Vector3(column, 0));
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
                    Hand[handIndex].Play(new Vector3(upper, 0));
                    Hand.RemoveAt(handIndex);

                    i = 7;
                    return true;
                }
                else if (!field.tiles[lower, 0].GetComponent<TileScript>().occupier) {
                    Hand[handIndex].playerOwned = false;
                    Hand[handIndex].Play(new Vector3(lower, 0));
                    Hand.RemoveAt(handIndex);
                    ownUnits.Add(Hand[handIndex].GetComponent<Unit>());
                    i = 7;
                    return true;
                }
            }
        }
        CalculateThreat();
        return false;
    }
    public void EndTurn() {

    }

    List<Unit> CalculateThreat() {
        for (int i = 0; i < playerUnits.Count; i++) {
            playerUnits[i].threat = playerUnits[i].value * Mathf.Pow((7 - playerUnits[i].position.y), 2);
            float dist = 0;
            for (int j = 0; j < ownUnits.Count; j++) {
                dist = Mathf.Pow(10 - Vector2.Distance(playerUnits[i].position, ownUnits[j].position), 1.15f);
                playerUnits[i].threat -= dist; //plus ownUnits[j].value modified by some value
            }

            /*
            if (playerUnits[i].threat > targetThreat) {
                target = playerUnits[i];
                targetThreat = target.threat;
            }
            */
        }
        List<Unit> rtn = CardSort.QuickSort(playerUnits.ConvertAll(x => (Card)x), 0, playerUnits.Count - 1).ConvertAll(x => (Unit)x);
        return rtn;
    }
}

