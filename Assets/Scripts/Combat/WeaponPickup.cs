using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] float respawnTime = 5;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                // Destroy(gameObject);
                StartCoroutine(HideForSeconds(respawnTime));
            }
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }

        private void ShowPickup(bool shoudldShow = false)
        {
            GetComponent<Collider>().enabled = shoudldShow;
            //method one to disable tree element
            transform.GetChild(0).gameObject.SetActive(shoudldShow);

            //method two to disable ALL children tree element
            foreach (Transform children in transform)
            {
                children.gameObject.SetActive(shoudldShow);
            }


        }
    }
}
