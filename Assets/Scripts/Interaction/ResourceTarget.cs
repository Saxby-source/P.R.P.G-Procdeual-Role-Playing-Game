using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Inventory;

namespace RPG.Interaction
{

    public class ResourceTarget : InteractTarget
    {
        //[SerializeField] GameObject resourceMesh;
        [SerializeField] int maxHitPoints = 3;
        [SerializeField] Item item;
        int currentHitPoints;
        bool hasHarvested = false;


        private void Start()
        {
            currentHitPoints = maxHitPoints;
        }

        public bool HasHarvested()
        {
            return hasHarvested;
        }

        public void TakeHitPoints(int amountToTakeOff)
        {
            currentHitPoints = Mathf.Max(currentHitPoints - amountToTakeOff, 0);
            if(currentHitPoints == 0)
            {
                ResourceCollect();
            }
        }

        private void ResourceCollect()
        {
            if(!hasHarvested)
            {
                // This is where to handle inventory stuff
                print("Harvested " + name);
                if (item != null)
                {
                    Inventory.Inventory.instance.Add(item);
                }
                Destroy(gameObject);
            }
        }
    }

}