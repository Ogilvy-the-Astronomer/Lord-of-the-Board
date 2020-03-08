using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Card {
    [field: SerializeField]
    public bool IsStructure { get; private set; }
    public int damage;
    [SerializeField]
    private int baseDamage;
    public int health;
    [field: SerializeField]
    public int MaxHealth { get; private set; }
    public int Movespeed;
    public int moveCount;

    public Vector2 position;

    public Enhancement enhancement;

    [SerializeField]
    protected TextMesh healthMesh;

    [SerializeField]
    protected TextMesh attackMesh;

    // Use this for initialization
    override protected void Start () {
        base.Start();
        health = MaxHealth;
        if (IsStructure) {
            baseDamage = 0;
            value = MaxHealth;
            Movespeed = 0;
        }
        else {
            value = damage * MaxHealth;
        }
        damage = baseDamage;
    }
	
	// Update is called once per frame
	protected override void Update () {
        if (transform.position.y < -10) {
            Move();
        }
        healthMesh.text = health.ToString();
        attackMesh.text = damage.ToString();
        base.Update();
    }


    public void Move() {
        transform.position = field.tiles[(int)position.x, (int)position.y].transform.position;
        transform.position -= new Vector3(0, transform.position.y + 0.75f, 0);
        field.tiles[(int)position.x, (int)position.y].GetComponent<TileScript>().occupier = this;
    }

    public void Attack(Unit enemy) {
        enemy.TakeDamage(damage);
        print(transform.name + " attacks " + enemy.transform.name);
    }

    public void TakeDamage(int _damage) {
        health -= _damage;
        if (health < 1) {
            Destroy();
        }
        
    }

    public override void Play(Vector2 _position) {
        base.Play(_position);
        field.cards.Add(this);
        position = _position;
        onField = true;
        Unit thisUnit = GetComponent<Unit>();
        field.tiles[(int)position.x, (int)position.y].GetComponent<TileScript>().occupier = thisUnit;
        Move();
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(onField);
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
        Player player = FindObjectOfType<Player>();
        if (playerOwned) {
            for (int i = 0; i < ai.playerUnits.Count; i++) {
                if (ai.playerUnits[i] == gameObject.GetComponent<Card>()) {
                    ai.playerUnits.RemoveAt(i);
                }
            }
            for (int i = 0; i < player.units.Count; i++) {
                if (player.units[i] == gameObject.GetComponent<Card>()) {
                    player.units.RemoveAt(i);
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
        onField = false;
        if (ability) ability.OnDestroy();

        //print("broke");
        if (enhancement) {
            //print("enhancement will be destroyed");
            enhancement.Destroy();
        }

        if(!onField) Destroy(gameObject);
    }

    public bool Forward() {
        if (moveCount < Movespeed) {
            if (!field.tiles[(int)position.x, (int)position.y - 1].GetComponent<TileScript>().occupier) {
                field.tiles[(int)position.x, (int)position.y].GetComponent<TileScript>().occupier = null;
                if (position.y > 0) {
                    position.y--;
                    Move();
                    moveCount++;
                    return true;
                }
            }
        }
        return false;
    }
    public bool Back() {
        if (moveCount < Movespeed) {
            if (!field.tiles[(int)position.x, (int)position.y + 1].GetComponent<TileScript>().occupier) {
                field.tiles[(int)position.x, (int)position.y].GetComponent<TileScript>().occupier = null;
                if (position.y < 6) {
                    position.y++;
                    Move();
                    moveCount++;
                    return true;
                }
            }
        }
        return false;
    }
    public bool Right() {
        if (moveCount < Movespeed) {
            if (!field.tiles[(int)position.x + 1, (int)position.y].GetComponent<TileScript>().occupier) {
                field.tiles[(int)position.x, (int)position.y].GetComponent<TileScript>().occupier = null;
                if (position.x < 6) {
                    position.x++;
                    Move();
                    moveCount++;
                    return true;
                }
            }
        }
        return false;
    }
    public bool Left() {
        if (moveCount < Movespeed) {
            if (!field.tiles[(int)position.x - 1, (int)position.y].GetComponent<TileScript>().occupier) {
                field.tiles[(int)position.x, (int)position.y].GetComponent<TileScript>().occupier = null;
                if (position.x > 0) {
                    position.x--;
                    Move();
                    moveCount++;
                    return true;
                }
            }
        }
        return false;
    }
}
