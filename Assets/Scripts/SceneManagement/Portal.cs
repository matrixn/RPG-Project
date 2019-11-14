using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.SceneManagement
{

    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E, F
        }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 2f;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float fadeWaitTime = 0.7f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to load is not set!");
                yield break;
            }

            DontDestroyOnLoad(gameObject);

            //star fade out
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);

            //load async next scene
            yield return SceneManager.LoadSceneAsync(sceneToLoad);


            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            //wait betwen fade out and fade in
            yield return new WaitForSeconds(fadeWaitTime);

            yield return fader.FadeIn(fadeInTime);

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;

                return portal;
            }

            return null;
        }
    }
}