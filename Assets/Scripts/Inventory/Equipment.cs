using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Inventory
{
    //[CreateAssetMenu(fileName = "New equipment", menuName = "Item/Equipment")]
    public class Equipment : Item
    {
        [SerializeField] EquipmentSlot equipSlot;

        public override void Use()
        {
            base.Use();

            EquipmentManager.instance.Equip(this);
            PlayerManager.instance.player.GetComponent<Combat.Fighter>();
            RemoveFromInventory();
        }

        public EquipmentSlot GetEquipSlot()
        {
            return equipSlot;
        }

    }
}

public enum EquipmentSlot {  Head, Chest, Legs, LeftHand, RightHand, Feet }
