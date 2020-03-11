using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Print : Ability {
    // Start is called before the first frame update
    protected override void Start() {
        activateConditions = false;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }

    public override void OnDestroy() {
        print("oof");
    }

    public override void Activate() {
        BritishRobot[] britishRobots = FindObjectsOfType<BritishRobot>();
        for (int i = 0; i < britishRobots.Length; i++) {
            if (britishRobots[i].playerOwned == parent.playerOwned 
                && britishRobots[i].MaxHealth > britishRobots[i].health) {
                britishRobots[i].health++;
            }
        }
    }

    public override int UtilityFunction() {
        int rtn = 0;
        activateConditions = false;
        AI ai = FindObjectOfType<AI>();
        BritishRobot britishRobot = null;
        for (int i = 0; i < ai.Hand.Count; i++) {
            britishRobot = ai.Hand[i].GetComponent<BritishRobot>();
            if (britishRobot) {
                ai.Hand[i].valueMod += 5;
                rtn--;
            }
        }
        britishRobot = null;
        for (int i = 0; i < ai.ownUnits.Count; i++) {
            britishRobot = ai.ownUnits[i].GetComponent<BritishRobot>();
            if (britishRobot && britishRobot.playerOwned == parent.playerOwned && britishRobot.health < britishRobot.MaxHealth) {
                rtn++;
                activateConditions = true;
            }
        }
        return rtn;
    }
}
