using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationManager : MonoBehaviour
{
    private float _timeToExit;
    private List<Agent> _agents = new();

    [SerializeField] private GameObject _agentPrefab;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _numberOfAgents;
    [SerializeField] private Text _timeText;

    private void Start()
    {
        for (int i = 0; i < _numberOfAgents; i++)
        {
            GameObject agentGameObject = Instantiate(_agentPrefab, _spawnPoint.position, Quaternion.identity);
            Agent agent = agentGameObject.GetComponent<Agent>();
            agent.InitializeAgent(_target);
            _agents.Add(agent);
        }
    }

    private void Update()
    {
        _timeToExit += Time.deltaTime;
        _timeText.text = $"Time: {_timeToExit:F2}";
    }
}
