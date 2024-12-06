using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[Serializable]
public class Point
{
    public Transform SpawnPoint;
    public Transform TargetPoint;
    public int NumberOfAgents;
}

public class SimulationManager : MonoBehaviour
{
    
    private float _timeToExit;
    private List<Agent> _agents = new();

    [SerializeField] private GameObject _agentPrefab;
    [SerializeField] private Text _timeText;
    [SerializeField] private Point[] _spawnPoints;

    private void Start()
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            SpawnAgents(spawnPoint.SpawnPoint, spawnPoint.TargetPoint, spawnPoint.NumberOfAgents);
        }
    }

    private void SpawnAgents(Transform spawnPoint, Transform targetPoint, int numberOfAgents)
    {
        for (int i = 0; i < numberOfAgents; i++)
        {
            var randomPos = new Vector3(Random.Range(-0.01f, 0.01f), 0, Random.Range(-0.01f, 0.01f));
            GameObject agentGameObject = Instantiate(_agentPrefab, spawnPoint.position + randomPos, Quaternion.identity);
            Agent agent = agentGameObject.GetComponent<Agent>();
            agent.InitializeAgent(targetPoint);
            _agents.Add(agent);
        }
    }

    private void Update()
    {
        int count = 0;
        foreach (var agent in _agents)
        {
            if (agent != null)
                count++;
        }
        if (count == 0) return;
        _timeToExit += Time.deltaTime;
        _timeText.text = $"Time: {_timeToExit:F2}";
    }
}
