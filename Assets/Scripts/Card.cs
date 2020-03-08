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

    public virtual void Play(Vector2 _position) {
        Play();
    }

    public virtual void Play() {

    }
}
