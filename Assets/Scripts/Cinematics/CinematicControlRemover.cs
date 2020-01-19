using RPG.Control;
using RPG.Core;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;
        private void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
            player = GameObject.FindWithTag("Player");
        }

        //rewatch the lesson #65 
        void DisableControl(PlayableDirector pd)
        {
            print("Disable Control");

            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;

        }

        void EnableControl(PlayableDirector pd)
        {
            print("Enable Control");
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}

