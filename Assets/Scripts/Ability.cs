using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour{
    public bool activateConditions;

    protected Field field;

    // Start is called before the first frame update
    protected virtual void Start() {
        field = FindObjectOfType<Field>();
        activateConditions = false;
    }

    // Update is called once per frame
    protected virtual void Update() {
        
    }

    public virtual void Activate() {

    }

    public virtual void OnTurnStart() {

    }

    public virtual void OnTurnEnd() {

    }

    public virtual void OnPlay() {

    }

}
