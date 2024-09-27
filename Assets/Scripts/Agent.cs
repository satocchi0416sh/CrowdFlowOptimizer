using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    [SerializeField] 
    private NavMeshAgent _navMeshAgent;
    private Transform _target;

    public void InitializeAgent(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target != null)
        {
            _navMeshAgent.SetDestination(_target.position);
        }
    }
}
