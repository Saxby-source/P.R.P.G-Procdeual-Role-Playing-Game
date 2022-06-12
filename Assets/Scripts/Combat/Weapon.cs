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

        public override void Use()
        {
            base.Use();

            Transform handTransform = PlayerManager.instance.player.GetComponent<Fighter>().GetHandTransform();
            Animator playerAnimator = PlayerManager.instance.player.GetComponent<Animator>();
            Spawn(handTransform, playerAnimator);
        }

        public void Spawn(Transform handTransform, Animator anim)
        {
            if (modelPrefab != null)
            {
                Instantiate(modelPrefab, handTransform);
            }
            if (animOverride != null)
            {
                anim.runtimeAnimatorController = animOverride;
            }
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