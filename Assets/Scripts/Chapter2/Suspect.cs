using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Suspect : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform[] destinations;
    [SerializeField] private GameObject[] clues;
    [SerializeField] private Animator animator;

    [SerializeField] private PlayerInput.ActionEvent onDestinationReached;


    private void Start()
    {
        agent.SetDestination(destinations[Random.Range(0, destinations.Length)].position);
    }

    private void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude > 0.2f ? 1f : 0f);
    }

    private void LateUpdate()
    {
        var dist = Vector3.Distance(transform.position, agent.destination);
        if (dist < 0.2f)
        {
            // on suspect reached his destination
            onDestinationReached.Invoke(default);
            gameObject.SetActive(false);
        }
        
    }
}
