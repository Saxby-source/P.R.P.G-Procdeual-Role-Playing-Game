using UnityEngine;
using System;
using RPG.Core;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {    

        Health target;                  // Reference to the target (only pointing the health component to sav eon resources)
        float timeSinceLastAttack = 0;  // Used as a counter for attack speed                
        [SerializeField] Transform handTransform = null;
        [SerializeField] Weapon deafultWeapon;
        [SerializeField] Weapon currentRightHandWeapon = null;

        private void Update()
        {
            EquipmentChange();

            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;     // If no target at all bounce out
            if (target.IsDead()) return;    // If target is already dead bounce out

            if (!GetIsInRange())            // If target is not in range move closer
            {
                GetComponent<Motor>().MoveTo(target.transform.position);
            }
            else // If target is in range then stop player motor and attack
            {
                GetComponent<Motor>().Cancel();
                AttackBehaviour();
            }
        }

        private void EquipmentChange()
        {
            if (Inventory.EquipmentManager.instance.GetRightHandAsWeapon() == null)
            {
                currentRightHandWeapon = deafultWeapon;
            }
            else if (Inventory.EquipmentManager.instance.GetRightHandAsWeapon() != null)
            {
                if (Inventory.EquipmentManager.instance.GetRightHandAsWeapon() != currentRightHandWeapon)
                {
                    currentRightHandWeapon = Inventory.EquipmentManager.instance.GetRightHandAsWeapon();
                    EquipWeapon(currentRightHandWeapon);
                }
            }
        }

        private void AttackBehaviour()
        {
            float currentTime = Time.deltaTime;

            if(timeSinceLastAttack > currentRightHandWeapon.GetSpeed())
            {
                // This will trigger Hit() event
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;                
            }
        }
        void Hit() // Animation Event wont find any references
        {
            if (target != null)
            {
                target.TakeDamage(currentRightHandWeapon.GetDamage());
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentRightHandWeapon.GetRange();
        }

        public Transform GetHandTransform()
        {
            return handTransform;
        }

        private void EquipWeapon(Weapon weapon)
        {
            currentRightHandWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            currentRightHandWeapon.Spawn(handTransform, animator);
        }

        public void Attack(CombatTarget target)
        {
            GetComponent<ActionScheduler>().StartAction(this);            
            this.target = target.GetComponent<Health>();            
        }

        public void Cancel()
        {
            GetComponent<Animator>().SetTrigger("stopInteract");
            target = null;
        }
                
    }
}
