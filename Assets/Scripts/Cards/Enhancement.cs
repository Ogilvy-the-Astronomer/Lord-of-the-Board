﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enhancement : Card{
    public int HealthBoost;
    public int AttackBoost;

    public Unit host;

    // Start is called before the first frame update
    override protected void Start(){
        base.Start();
    }

    // Update is called once per frame
    override protected void Update(){
        base.Update();
        if (host) { transform.position = host.transform.position; }
    }

    public override void Play(Vector2 _position) {
        base.Play();
        onField = true;
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(onField);
        }
        host = field.tiles[(int)_position.x, (int)_position.y].GetComponent<TileScript>().occupier.GetComponent<Unit>();
        host.damage += AttackBoost;
        host.health += HealthBoost;
        host.enhancement = this;
        GetComponent<BoxCollider>().enabled = false;
    }

    public void Destroy() {
        print("enhancement destroyed");
        for (int i = 0; i < field.cards.Count; i++) {
            if (field.cards[i] == gameObject.GetComponent<Card>()) {
                field.cards.RemoveAt(i);
            }
        }
        onField = false;
        if (ability) ability.OnDestroy();
        if (!onField) Destroy(gameObject);
    }

    public override int UtilityFunction() {
        return 0;
    }
}
