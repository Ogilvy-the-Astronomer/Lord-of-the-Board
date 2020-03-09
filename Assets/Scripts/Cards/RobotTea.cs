using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotTea : Spell {
    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        base.Update();
    }

    public override int UtilityFunction() {
        value = ability.UtilityFunction();
        return (int)value;
    }

    public override void Play() {
        base.Play();
        print("tea time");
    }
}
