using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

enum NavAgentState
{
    Inactive,
    Moving,
    Waiting,
}

public class NavAgent : MonoBehaviour
{
    // movement
    [Header("Movement")]
    // The maximum speed that the enemy can move
    [SerializeField] private float maxMoveSpeed = 2.0f;

    [Header("Navigation")]
    // The zones that the enemy may decide to move to
    [SerializeField]
    private ZoneHolder zoneHolder;
    private float _totalWeight;
    private NavMeshAgent _agent;
    private NavAgentState _state = NavAgentState.Waiting;
    private bool _isWaitCoroutineRunning = false;
    private int _currentZoneIndex = -1;

    [Header("Wait Times")]
    // The wait times for when an enemy has reached a location
    // pick a random time between the min and max to wait (in seconds)
    [SerializeField] private float minWaitTime = 2.0f;
    [SerializeField] private float maxWaitTime = 7.0f;



    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        if (zoneHolder == null || zoneHolder.zones.Count == 0)
        {
            _state = NavAgentState.Inactive;
        }
        else
        {
            // calculate the total weight
            foreach(var zone in zoneHolder.zones)
            {
                _totalWeight += zone.weight;
            }

            // set the initial wait time to just be the minimum wait time
            // 5 second grace period for players to start
            EnterWaitingState();
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case NavAgentState.Moving:
                HandleMoving();
                break;
            case NavAgentState.Waiting:
                HandleWaiting();
                break;
            case NavAgentState.Inactive: // we don't do anything if we are inactive
            default:
                break;
        }
    }

    void EnterMovingState()
    {
        _state = NavAgentState.Moving;

        // pick new location
        // set the location on the NavMeshAgent
        _agent.SetDestination(PickNewLocation());
        _agent.speed = maxMoveSpeed;
    }

    void EnterWaitingState()
    {
        _state = NavAgentState.Waiting;
    }

    void HandleMoving()
    {
        // check if we have reached the destination
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            // we have reached the destination
            // enter waiting state
            EnterWaitingState();
        }
    }

    void HandleWaiting()
    {
        // check if we have waited long enough
        if (!_isWaitCoroutineRunning)
        {
            StartCoroutine(DoWait());
        }
    }

    IEnumerator DoWait()
    {
        _isWaitCoroutineRunning = true;
        yield return new WaitForSeconds(Mathf.Lerp(minWaitTime, maxWaitTime, Random.Range(0.0f, 1.0f)));
        _isWaitCoroutineRunning = false;
        EnterMovingState();
    }

    Vector2 PickNewLocation()
    {
        float random = Random.Range(0, _totalWeight);

        // make sure we can't select the same zone twice
        var zones = new List<ZoneWeight>(zoneHolder.zones);
        if (_currentZoneIndex != -1)
        {
            zones.RemoveAt(_currentZoneIndex);
        }

        var collider = zones.Last().zone;
        _currentZoneIndex = zones.Count - 1;

        float sum = 0.0f;
        for (int i = 0; i < zones.Count; ++i)
        {
            var zone = zones[i];
            var nextSum = sum + zone.weight;
            if (random >= sum && random < nextSum)
            {
                // found the zone to go to
                collider = zone.zone;
                _currentZoneIndex = i;
                break;
            }
        }

        var bounds = collider.bounds;

        var output = new Vector2(
            Mathf.Lerp(bounds.min.x, bounds.max.x, Random.Range(0.0f, 1.0f)),
            Mathf.Lerp(bounds.min.y, bounds.max.x, Random.Range(0.0f, 1.0f)));
        return output;
    }
}
