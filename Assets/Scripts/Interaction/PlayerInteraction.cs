using UnityEngine;
using RPG.Core;
using RPG.Movement;
using RPG.Inventory;
using System;

namespace RPG.Interaction
{
    public class PlayerInteraction : MonoBehaviour, IAction
    {
        [SerializeField] float interactRange = 1f;
        [SerializeField] int harvestDamage = 1;
        [SerializeField] float harvestSpeed = 1f;
        InteractTarget target;

        float timeSinceLastStrike = 0;  // Used as a counter for harvest speed

        private void Update()
        {
            timeSinceLastStrike += Time.deltaTime;

            if (target == null) return;     // If no target at all bounce out
            if (target.GetComponent<ResourceTarget>().HasHarvested()) return;    // check if its a resource thats already been harvested

            if (!GetIsInRange())            // If target is not in range move closer
            {
                GetComponent<Motor>().MoveTo(target.transform.position);
            }
            else // If target is in range then stop player motor and interact
            {
                GetComponent<Motor>().Cancel(); // Cancels motor IAction

                if(target.GetComponent<ResourceTarget>())
                {
                    HarvestBehaviour();
                }
                if(target.GetComponent<ItemTarget>())
                {
                    // Pick up behaviour AKA play pick up animation, sounds, affects etc
                }
            }
        }

        // START: resource interaction
        void HarvestHit() // Animation Event wont find any references
        {
            target.GetComponent<ResourceTarget>().TakeHitPoints(harvestDamage);
        }
        private void HarvestBehaviour()
        {
            float currentTime = Time.deltaTime;

            if (timeSinceLastStrike > harvestSpeed)
            {
                // This will trigger HarvestHit() event
                GetComponent<Animator>().SetTrigger("harvest");
                timeSinceLastStrike = 0;
            }
        }
        public void Harvest(ResourceTarget target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            this.target = target.GetComponent<InteractTarget>();
        }
        // END 

        // START: General Methods
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < interactRange;
        }
        public void Cancel() // For IAction
        {
            GetComponent<Animator>().SetTrigger("stopInteract");
            target = null;
        }
        // END 

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(gameObject.transform.position, interactRange);
        }
    }
}
