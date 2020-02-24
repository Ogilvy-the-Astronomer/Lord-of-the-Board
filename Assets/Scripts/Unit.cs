using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Card {
    public int damage;
    public int health;

	// Use this for initialization
	override protected void Start () {
        base.Start();
        value = damage * health;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < -100) {
            Move();
        }
	}

    override public void Move() {
        base.Move();
    }

    public bool Forward() {
        if (!field.tiles[(int)position.x, (int)position.y - 1].GetComponent<TileScript>().occupier) {
            field.tiles[(int)position.x, (int)position.y].GetComponent<TileScript>().occupier = null;
            if (position.y > 0) {
                position.y--;
                Move();
                return true;
            }
        }
        return false;
    }
    public bool Back() {
        if (!field.tiles[(int)position.x, (int)position.y + 1].GetComponent<TileScript>().occupier) {
            field.tiles[(int)position.x, (int)position.y].GetComponent<TileScript>().occupier = null;
            if (position.y < 6) {
                position.y++;
                Move();
                return true;
            }
        }
        return false;
    }
    public bool Right() {
        if (!field.tiles[(int)position.x + 1, (int)position.y].GetComponent<TileScript>().occupier) {
            field.tiles[(int)position.x, (int)position.y].GetComponent<TileScript>().occupier = null;
            if (position.x < 6) {
                position.x++;
                Move();
                return true;
            }
        }
        return false;
    }
    public bool Left() {
        if (!field.tiles[(int)position.x - 1, (int)position.y].GetComponent<TileScript>().occupier) {
            field.tiles[(int)position.x, (int)position.y].GetComponent<TileScript>().occupier = null;
            if (position.x > 0) {
                position.x--;
                Move();
                return true;
            }
        }
        return false;
    }
}
