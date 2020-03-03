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

    public Ability ability;
    public bool AcivateConditions { get; protected set; }

    //public Vector2 position;
    // Use this for initialization
    protected virtual void Start () {
        for (int i = 0; i < transform.childCount; i++) {
            if(!transform.GetChild(i).GetComponent<TextMesh>() || !transform.GetComponent<Card>().playerOwned) transform.GetChild(i).gameObject.SetActive(onField);
        }
        //print(transform.childCount);

    }
	
	// Update is called once per frame
	protected virtual void Update () {
        if(ability) AcivateConditions = ability.activateConditions;
    }

    /*
    public virtual void Move() {
        transform.position = field.tiles[(int)position.x, (int)position.y].transform.position;
        transform.position -= new Vector3(0, transform.position.y + 0.75f, 0);
        //threat = value * Mathf.Pow((7 - position.y), 2);
        field.tiles[(int)position.x, (int)position.y].GetComponent<TileScript>().occupier = this;
    }
    */

    public virtual void Play(Vector2 _position) {
        Play();
    }

    public virtual void Play() {

    }
}
