using UnityEngine;
using UnityEngine.UI;

namespace RPG.Inventory
{    public class InventorySlot : MonoBehaviour
    {
        public Image icon;
        public Button removeButton;
        Item item;

        public void AddItem(Item newItme)
        {
            item = newItme;
            icon.sprite = item.icon;
            icon.enabled = true;
            removeButton.interactable = true;

        }

        public void ClearSlot()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            removeButton.interactable = false;
        }

        public void RemoveItemFromInventory()
        {
            Inventory.instance.Remove(item);
        }

        public void UseItem()
        {
            if (item != null)
            {
                item.Use();
            }
        }
    }
}
