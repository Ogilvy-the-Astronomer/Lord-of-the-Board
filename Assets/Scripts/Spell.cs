﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : Card {
    // Start is called before the first frame update
    override protected void Start() {
        
    }

    // Update is called once per frame
    override protected void Update(){
        base.Update();
    }

    public override void Play() {
        base.Play();
        ability.Activate();
    }
}
