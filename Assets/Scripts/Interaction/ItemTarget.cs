using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Interaction
{
    public class ItemTarget : InteractTarget
    {
        [SerializeField] public Inventory.Item itemData;

        public void PickUp()
        {
            Debug.Log("Picked up " + itemData.name);
            Inventory.Inventory.instance.Add(itemData);
            Destroy(gameObject);
            
        }
    }
}
