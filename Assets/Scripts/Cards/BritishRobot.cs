using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BritishRobot : Unit {
    // Start is called before the first frame update
    protected override void Start() {
        MaxHealth = 3;
        Movespeed = 1;
        baseDamage = 1;
        IsStructure = false;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }
}
