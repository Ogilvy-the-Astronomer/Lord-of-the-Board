using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : Unit {
    // Start is called before the first frame update
    protected override void Start(){
        base.Start();
        field.cards.Add(this);
    }

    // Update is called once per frame

    override protected void Update(){
        base.Update();
    }
}
