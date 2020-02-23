using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Card : MonoBehaviour {
    [SerializeField]
    protected Field field;

    public bool onField;

    public float threat;
    public float value;
    public bool playerOwned;

    public Vector2 position;
    // Use this for initialization
    virtual protected void Start () {
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(onField);
        }
        //print(transform.childCount);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    virtual public void Move() {
        transform.position = field.tiles[(int)position.x, (int)position.y].transform.position;
        transform.position -= new Vector3(0, transform.position.y + 0.75f, 0);
        //threat = value * Mathf.Pow((7 - position.y), 2);
        field.tiles[(int)position.x, (int)position.y].GetComponent<TileScript>().occupier = this;
    }

    virtual public void Summon(Vector2 _position) {
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
}
