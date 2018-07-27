using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;
    public GameObject inventoryUI;
    public ThirdPersonCamera cameraScript;

    Inventory inventory;

    InventorySlot[] slots;

	// Use this for initialization
	void Start () {
        this.inventory = Inventory.instance;
        this.inventory.onItemChangedCallback += UpdateUI;
        this.slots = itemsParent.GetComponentsInChildren<InventorySlot>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Inventory"))
        {
            this.inventoryUI.SetActive(!this.inventoryUI.activeSelf);
            Camera.main.GetComponent<ThirdPersonCamera>().enabled = !Camera.main.GetComponent<ThirdPersonCamera>().enabled;
        }
	}

    void UpdateUI()
    {
        for(int i=0; i<slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                this.slots[i].AddItem(inventory.items[i]);
            } else
            {
                this.slots[i].ClearSlot();
            }
        }
    }
}
