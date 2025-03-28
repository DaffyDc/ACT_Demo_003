using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITest : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent?.SetDestination(target.position);
    }
}
