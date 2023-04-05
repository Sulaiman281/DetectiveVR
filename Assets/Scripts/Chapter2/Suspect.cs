using UnityEngine;
using UnityEngine.AI;

public class Suspect : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform[] randDestinationSpots;
    [SerializeField] private Animator animator;
    

    private void Update()
    {
        if (!agent.hasPath) return;
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void PickDestination()
    {
        agent.SetDestination(randDestinationSpots[Random.Range(0, randDestinationSpots.Length)].position);
    }
}
