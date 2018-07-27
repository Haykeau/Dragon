using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachMesh : MonoBehaviour {

    public GameObject hand;
    GameObject item;
    bool triggered = false;

    // Use this for initialization
    void Start () {
        this.item = this.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (triggered)
        {
            if (Input.GetButtonDown("Use"))
            {
                Debug.Log("Trying to get");
                item.transform.parent = hand.transform;
                item.transform.position = hand.transform.position;
                item.transform.rotation = hand.transform.rotation;
                this.GetComponent<BoxCollider>().enabled = false;
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("In front of sword");
            triggered = true;
        }
    }
}
