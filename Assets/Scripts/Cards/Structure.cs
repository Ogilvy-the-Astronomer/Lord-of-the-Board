using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : Card {

    public int health;
    [field: SerializeField]
    public int MaxHealth { get; private set; }

    public Vector2 position;

    [SerializeField]
    protected TextMesh healthMesh;
    // Start is called before the first frame update
    override protected void Start() {
        base.Start();
        health = MaxHealth;
        value = MaxHealth;
    }

    // Update is called once per frame
    override protected void Update(){
        if (transform.position.y < -10) {
            Move();
        }
        healthMesh.text = health.ToString();
        base.Update();
    }

    public void Move() {
        transform.position = field.tiles[(int)position.x, (int)position.y].transform.position;
        transform.position -= new Vector3(0, transform.position.y + 0.75f, 0);
        //threat = value * Mathf.Pow((7 - position.y), 2);
        field.tiles[(int)position.x, (int)position.y].GetComponent<TileScript>().occupier = this;
    }

    public void TakeDamage(int _damage) {
        health -= _damage;
        if (health < 1) {
            Destroy(gameObject);
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
}
