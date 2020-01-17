using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject equipedPrefab = null;
        [SerializeField] AnimatorOverrideController animatorOverride = null;

        [SerializeField] float weaponRange = 5f;
        [SerializeField] float weaponDamage = 5f;

        [SerializeField] bool isRightHanded = true;


        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (equipedPrefab != null)
            {

                //decide which hand to use
                Transform handTransform;
                if (isRightHanded) handTransform = rightHand;
                else handTransform = leftHand;

                Instantiate(equipedPrefab, handTransform);
            }

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

        public float GetDamage()
        {
            return weaponDamage;
        }

        public float GetRange()
        {
            return weaponRange;

        }
    }
}