using System;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make new weapon", order = 0)]
    public class Weapon : Inventory.Equipment
    {
        [SerializeField] float damage = 10f;
        [SerializeField] float range = 2f;
        [SerializeField] float attackSpeed = 1f; // To be moved to a stats script;
        [SerializeField] AnimatorOverrideController animOverride = null;
        [SerializeField] GameObject modelPrefab = null;

        const string weaponName = "Weapon";

        public override void Use()
        {
            base.Use();

            Transform handTransform = PlayerManager.instance.player.GetComponent<Fighter>().GetHandTransform();
            Animator playerAnimator = PlayerManager.instance.player.GetComponent<Animator>();
            Spawn(handTransform, playerAnimator);
        }

        public void Spawn(Transform handTransform, Animator anim)
        {
            DeSpawnOldWeapon(handTransform);

            if (modelPrefab != null)
            {
                GameObject weapon = Instantiate(modelPrefab, handTransform);
                weapon.name = weaponName;
            }
            if (animOverride != null)
            {
                anim.runtimeAnimatorController = animOverride;
            }
        }

        private void DeSpawnOldWeapon(Transform handTransform)
        {
            Transform oldWeapon = handTransform.Find(weaponName);
            if (oldWeapon == null) return;

            oldWeapon.name = "if you see this theres an error";
            Destroy(oldWeapon.gameObject);
        }

        public GameObject GetVisuals()
        {
            return modelPrefab;
        }

        public float GetDamage()
        {
            return damage;
        }

        public float GetRange()
        {
            return range;
        }

        public float GetSpeed()
        {
            return attackSpeed;
        }
    }
}