using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private float _beamLength = 1f;
    [SerializeField] private LayerMask _targetLayer;
    private Transform _target;
    private float _defaultSpeed;

    private bool _isStopped = false;

    public void InitializeAgent(Transform target)
    {
        _target = target;
        _defaultSpeed = _navMeshAgent.speed;
    }

    private void Update()
    {
        if (_target != null)
        {
            _navMeshAgent.SetDestination(_target.position);
        }

        ShootBeam();
    }

    private void ShootBeam()
    {
        Ray ray = new Ray(transform.position + Vector3.up, transform.forward);
        RaycastHit hit;

        Debug.DrawRay(transform.position + Vector3.up, transform.forward * _beamLength, Color.red);

        if (Physics.Raycast(ray, out hit, _beamLength, _targetLayer))
        {
            if (hit.collider.CompareTag("Agent"))
            {
                StopAgent();
                return;
            }
        }

        ResumeAgent();
    }

    private void StopAgent()
    {
        if (!_isStopped)
        {
            _navMeshAgent.speed = 0f;
            _isStopped = true;
        }
    }

    private void ResumeAgent()
    {
        if (_isStopped)
        {
            _navMeshAgent.speed = _defaultSpeed;
            _isStopped = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            Destroy(gameObject);
        }
    }
}