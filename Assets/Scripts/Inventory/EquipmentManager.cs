using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Inventory
{
    public class EquipmentManager : MonoBehaviour
    {
        // Singleton
        #region
        public static EquipmentManager instance;

        private void Awake()
        {
            instance = this; ;
        }
        #endregion


        [SerializeField] Equipment[] currentEquipment; // Container for equipment data

        public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem); // delegate to handle UI changes
        public OnEquipmentChanged onEquipmentChanged;

        Inventory inventory; // Refernece to the instance of the players inventory

        private void Start()
        {
            inventory = Inventory.instance; // Get reference to instance of players inventory
            int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length; // Generate slots according to how many equipment slots there are *allows to be changed easier down the line
            currentEquipment = new Equipment[numSlots]; // init the equipment slots
        }

        public void Equip(Equipment newItem)
        {
            int slotIndex = (int)newItem.GetEquipSlot(); 

            Equipment oldItem = null;

            if (currentEquipment[slotIndex] != null)
            {
                oldItem = currentEquipment[slotIndex];                
                inventory.Add(oldItem);
            }

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(newItem, oldItem);
            }

            currentEquipment[slotIndex] = newItem;          
            
        }

        public void Unequip(int slotIndex)
        {
            if (currentEquipment[slotIndex] != null)
            {
                Equipment oldItem = currentEquipment[slotIndex];
                inventory.Add(oldItem);

                currentEquipment[slotIndex] = null;

                if (onEquipmentChanged != null)
                {
                    onEquipmentChanged.Invoke(null, oldItem);
                }
            }

        }

        public Combat.Weapon GetRightHandAsWeapon()
        {
            return (Combat.Weapon)currentEquipment[5]; // slot index 5 is Right hand equipment see enum for more
        }

        public void UnequipAll()
        {
            for (int i = 0; i < currentEquipment.Length; i++)
            {
                Unequip(i);
            }
        }


        public void Update()
        {
            // put button press for unequip all here

        }
    }
}
