using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName="Inventory/Menu")]
public class Item : ScriptableObject {

    public new string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public virtual void Use()
    {
        Debug.Log("Using" + this.name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
