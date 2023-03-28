using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    
    private float _speed;

    private bool _hasPurpose;

    public Vector3 destination
    {
        set
        {
            agent.SetDestination(value);
            _hasPurpose = true;
        }
    }

    private void Awake()
    {
        _hasPurpose = false;
    }

    private void OnValidate()
    {
        if (animator == null) animator = transform.GetComponent<Animator>();
        if (agent == null) agent = transform.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!_hasPurpose) return;
        if (agent.remainingDistance < .8f)
        {
            destination = Chapter2Manager.instance.RandDestination;
        }
        // var currentSpeed = Mathf.Approximately(agent.velocity.magnitude, 0f) ? 0f : 1f;
        animator.SetFloat("Speed", agent.velocity.magnitude > 0.1f ? 1f : 0f);
    }
}
