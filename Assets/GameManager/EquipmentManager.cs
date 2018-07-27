using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    #region Singleton

    public static EquipmentManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found");
            return;
        }

        instance = this;
    }

    #endregion

    public SkinnedMeshRenderer targetMesh;
    GameObject Armature;
    Equipment[] currentEquipment;
    MeshRenderer[] currentMesh;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;

    void Start()
    {
        this.inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        this.currentEquipment = new Equipment[numSlots];
        this.currentMesh = new MeshRenderer[numSlots];
    }

    public void Equip(Equipment equipment)
    {
        int slotIndex = (int) equipment.equipSlot;
        Equipment oldEquipment = null;

        if(this.currentEquipment[slotIndex] != null)
        {
            oldEquipment = currentEquipment[slotIndex];
            this.inventory.Add(oldEquipment);
        }

        if (this.onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(equipment, oldEquipment);
        }

        this.currentEquipment[slotIndex] = equipment;
        MeshRenderer newMesh = Instantiate<MeshRenderer>(equipment.mesh);

        GameObject[] stuffSlots = GameObject.FindGameObjectsWithTag("Stuff_Slots");

        for(int i = 0; i<stuffSlots.Length; i++)
        {
            Debug.Log(stuffSlots[i]);
        }

        Debug.Log(stuffSlots);

        switch (equipment.equipSlot)
        {
            case EquipmentSlot.Back:
                newMesh.transform.parent = stuffSlots[3].transform;
                stuffSlots[3].transform.GetChild(0).localPosition = Vector3.zero;
                stuffSlots[3].transform.GetChild(0).localEulerAngles = Vector3.zero;
                break;

            case EquipmentSlot.Chest:
                newMesh.transform.parent = stuffSlots[1].transform;
                stuffSlots[1].transform.GetChild(0).localPosition = Vector3.zero;
                stuffSlots[1].transform.GetChild(0).localEulerAngles = Vector3.zero;
                break;

            case EquipmentSlot.Belt:
                newMesh.transform.parent = stuffSlots[2].transform;
                stuffSlots[2].transform.GetChild(0).localPosition = Vector3.zero;
                stuffSlots[2].transform.GetChild(0).localEulerAngles = Vector3.zero;
                break;

            case EquipmentSlot.Weapon:
                newMesh.transform.parent = stuffSlots[0].transform;
                stuffSlots[0].transform.GetChild(0).localPosition = Vector3.zero;
                stuffSlots[0].transform.GetChild(0).localEulerAngles = Vector3.zero;
                break;

            default:
                Debug.Log("Error Enum Switch to equip equipment");
                break;

        }

        this.currentMesh[slotIndex] = newMesh;
    }

    public void Unequip(int slotIndex)
    {
        if(this.currentEquipment[slotIndex] != null)
        {
            if(currentMesh[slotIndex] != null)
            {
                Destroy(currentMesh[slotIndex].gameObject);
            }
            Equipment oldEquipment = currentEquipment[slotIndex];
            inventory.Add(oldEquipment);

            if (this.onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldEquipment);
            }

            currentEquipment[slotIndex] = null;
        }
    }

    public void UnequipAll()
    {
        for(int i = 0; i < this.currentEquipment.Length; i++)
        {
            this.Unequip(i);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            this.UnequipAll();
        }
    }

}
