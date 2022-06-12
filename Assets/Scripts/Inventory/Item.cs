using UnityEngine;

namespace RPG.Inventory
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item/Item")]
    public class Item : ScriptableObject
    {
        new public string name = "New Item";
        public Sprite icon = null;
        [HideInInspector] public bool isDefaultItem = false;

        public virtual void Use()
        {
            Debug.Log("trying to use " + name);
        }

        public void RemoveFromInventory()
        {
            Inventory.instance.Remove(this);
        }
    }
}
