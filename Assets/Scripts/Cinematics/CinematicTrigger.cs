using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {

        bool alreadyTrigered = false;

        private void OnTriggerEnter(Collider other)
        {
            if (!alreadyTrigered && other.gameObject.tag == "Player")
            {
                alreadyTrigered = true;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}