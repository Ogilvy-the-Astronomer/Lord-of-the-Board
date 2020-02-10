using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Card {
    [SerializeField]
    bool onField;
    public Vector2 position;
    public int damage;
    public int health;
	// Use this for initialization
	void Start () {
        value = damage * health;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
