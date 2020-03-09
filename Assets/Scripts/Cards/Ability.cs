using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour{
    public bool activateConditions;

    protected Field field;
    public Card parent;
    // Start is called before the first frame update
    protected virtual void Start() {
        field = FindObjectOfType<Field>();
        activateConditions = false;
    }

    // Update is called once per frame
    protected virtual void Update() {
        
    }
    public abstract int UtilityFunction();

    public virtual void Activate() {

    }

    public virtual void OnTurnStart() {

    }

    public virtual void OnTurnEnd() {

    }

    public virtual void OnOwnTurnStart() {

    }

    public virtual void OnOwnTurnEnd() {

    }

    public virtual void OnOtherTurnStart() {

    }

    public virtual void OnOtherTurnEnd() {

    }

    public virtual void OnPlay() {

    }

    public virtual void OnDestroy() {

    }

}
