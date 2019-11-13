using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        Transform target;
        [SerializeField] float maxSpeed = 5.66f;
        NavMeshAgent navMeshAgent;
        Animator animator;
        Health health;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();

            UpdateAnimator();
        }

        //moving as action, this will cancel previous actions (eg. attack) and that's why Fighter uses MoveTo at #19
        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            animator.SetFloat("ForwardSpeed", speed);

        }
    }

}