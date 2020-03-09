using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour {
    [SerializeField]
    AI ai;
    [SerializeField]
    Player player;

    [field: SerializeField]
    public bool PlayerTurn { get; private set; }
    // Start is called before the first frame update
    void Start(){
        ai = FindObjectOfType<AI>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void EndTurn() {
        if (PlayerTurn) {
            player.EndTurn();
        }
        else {
            ai.EndTurn();
        }
        
        Ability[] abilities = FindObjectsOfType<Ability>();
        for (int i = 0; i < abilities.Length; i++) {
            if (abilities[i].GetComponent<Card>().onField) {
                abilities[i].OnTurnEnd();
                if (abilities[i].GetComponent<Card>().playerOwned == PlayerTurn) {
                    abilities[i].OnOwnTurnEnd();
                }
                else {
                    abilities[i].OnOtherTurnEnd();
                }
            }
        }
        PlayerTurn = !PlayerTurn;
        StartNextTurn();
    }


    void StartNextTurn() {
        if (PlayerTurn) {
            player.StartTurn();
        }
        else {
            ai.StartTurn();
        }
        Ability[] abilities = FindObjectsOfType<Ability>();
        for (int i = 0; i < abilities.Length; i++) {
            if (abilities[i].GetComponent<Card>().onField) {
                abilities[i].OnTurnStart();
                if (abilities[i].GetComponent<Card>().playerOwned == PlayerTurn) {
                    abilities[i].OnOwnTurnStart();
                }
                else {
                    abilities[i].OnOtherTurnStart();
                }
            }
        }
    }
}
