using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Interaction;
using UnityEngine.EventSystems;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return; // Making sure the cursor is not over UI
            if (InteractionUpdate()) return;       // Combat interaction method         
            if (InteractMovement()) return;     // Player movement method

            //print("Nothing to do");
        }

        private bool InteractionUpdate()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                var target = hit.transform;

                if (target.GetComponent<CombatTarget>() != null)
                {
                    InteractCombat(target.GetComponent<CombatTarget>());
                }
                if (target.GetComponent<ResourceTarget>() != null)
                {
                    InteractResource(target.GetComponent<ResourceTarget>());
                }
                if (target.GetComponent<ItemTarget>() != null)
                {
                    InteractItem(target.GetComponent<ItemTarget>());
                }

                if (target.GetComponent<CombatTarget>() != null || target.GetComponent<ResourceTarget>() != null || target.GetComponent<ItemTarget>() != null) // Affordance check
                {
                    return true;
                }
            }
            return false;
        }

        private void InteractItem(ItemTarget target)
        {
            if(Input.GetMouseButtonDown(0))
            {
                target.PickUp();
            }
        }

        private void InteractResource(ResourceTarget target)
        {
            if (Input.GetMouseButtonDown(0))
            {
                transform.LookAt(target.transform);
                GetComponent<PlayerInteraction>().Harvest(target);
            }
        }

        private void InteractCombat(CombatTarget target)
        {
            if(Input.GetMouseButtonDown(0))
            {
                transform.LookAt(target.transform);
                GetComponent<Fighter>().Attack(target);
            }
        }

        private bool InteractMovement()
        {
            bool hasHit = Physics.Raycast(GetMouseRay(), out RaycastHit hit); // sotre raycast hit info, and make bool to test if/what it anything
            
            if (hasHit) // testing if it hit
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Motor>().StartMoveAction(hit.point); // Move to raycast hit position in world space                  
                }
                return true; // Outside of if statement for affordance, i.e still return true if hovering over
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
