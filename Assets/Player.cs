using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    GameObject currentObject;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            if (Physics.Raycast(GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit)) {
                if (hit.transform.GetComponent<Unit>() && hit.transform.GetComponent<Card>().playerOwned) {
                    currentObject = hit.transform.gameObject;
                }
            }
        }
    }

    public void Forward() {
        if (currentObject) {
            currentObject.GetComponent<Unit>().Forward();
        }
    }
    public void Back() {
        if (currentObject) {
            currentObject.GetComponent<Unit>().Back();
        }
    }
    public void Right() {
        if (currentObject) {
            currentObject.GetComponent<Unit>().Right();
        }
    }
    public void Left() {
        if (currentObject) {
            currentObject.GetComponent<Unit>().Left();
        }
    }
}
