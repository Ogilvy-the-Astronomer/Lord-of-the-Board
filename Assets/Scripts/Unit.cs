using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Card {
    public int damage;
    public int health;

    [SerializeField]
    TextMesh healthMesh;

    [SerializeField]
    TextMesh attackMesh;

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
        healthMesh.text = health.ToString();
        attackMesh.text = damage.ToString();
    }

    override public void Move() {
        base.Move();
    }

    public void Attack(Unit enemy) {
        enemy.TakeDamage(damage);
        print(transform.name + " attacks " + enemy.transform.name);
    }

    public void TakeDamage(int _damage) {
        health -= _damage;
        if (health < 1) {
            Destroy(gameObject);
        }
    }

    public void Destroy() {
        field.tiles[(int)position.x, (int)position.y].GetComponent<TileScript>().occupier = null;
        for (int i = 0; i < field.cards.Count; i++) {
            if (field.cards[i] == gameObject.GetComponent<Card>()) {
                field.cards.RemoveAt(i);
            }
        }
        AI ai = FindObjectOfType<AI>();
        if (playerOwned) {
            for (int i = 0; i < ai.playerUnits.Count; i++) {
                if (ai.playerUnits[i] == gameObject.GetComponent<Card>()) {
                    ai.playerUnits.RemoveAt(i);
                }
            }
        }
        else {
            for (int i = 0; i < ai.ownUnits.Count; i++) {
                if (ai.ownUnits[i] == gameObject.GetComponent<Card>()) {
                    ai.ownUnits.RemoveAt(i);
                }
            }
        }
        Destroy(this);
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
