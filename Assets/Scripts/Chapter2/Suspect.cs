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
    [SerializeField] private int maxCluesShow;

    [SerializeField] private PlayerInput.ActionEvent onDestinationReached;


    private void Start()
    {
        var count = 0;
        foreach (var clue in clues)
        {
            if (Random.Range(0, 2) == 1)
            {
                clue.SetActive(true);
                count++;
                if (count > Mathf.Min(maxCluesShow, clues.Length))
                {
                    break;
                }
            }
            clue.SetActive(false);
        }
        

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
