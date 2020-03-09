using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : Ability {
    // Start is called before the first frame update
    override protected void Start(){
        base.Start();
        activateConditions = true;
    }

    // Update is called once per frame
    protected override void Update(){
        
    }

    public override void Activate() {
        if (activateConditions) {
            print("BOOM");
            for (int i = 0; i < field.cards.Count; i++) {
                Unit unit = field.cards[i].GetComponent<Unit>();
                if (unit) {
                    unit.TakeDamage(1);
                }
            }
        }
    }

    public override int UtilityFunction() {
        return 1;
    }
}
