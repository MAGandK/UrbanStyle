using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaypointNavigator : MonoBehaviour
{
   [SerializeField] private CharacterNavigationController _controller;
    public Waypoint _currentWaypoint;

    private int _direction;

    private void Awake()
    {
        if (_controller == null)
        {
            _controller = GetComponent<CharacterNavigationController>();
        }
    }

    private void Start()
    {
        _direction = Mathf.RoundToInt(Random.Range(0f, 1f));
        _controller.SetDestination(_currentWaypoint.GetPosition(), _currentWaypoint.name);
    }

    private void Update()
    {
        if (_controller._IsReachedDestination)
        {
            bool shouldBranch = false;

            if (_currentWaypoint.Branches != null && _currentWaypoint.Branches.Count > 0)
            {
                float random = Random.Range(0f, 1f);
                shouldBranch = random <= _currentWaypoint._branchRatio;
            }

            if (shouldBranch)
            {
                _currentWaypoint = _currentWaypoint.Branches[Random.Range(0, _currentWaypoint.Branches.Count)];
            }
            else
            {
                if (_direction == 0)
                {
                    if (_currentWaypoint._nextWaypoint != null)
                    {
                        _currentWaypoint = _currentWaypoint._nextWaypoint;
                    }
                    else
                    {
                        _direction = 1;
                        _currentWaypoint = _currentWaypoint._previousWaypoint;
                    }
                }
                else if (_direction == 1)
                {
                    if (_currentWaypoint._previousWaypoint != null)
                    {
                        _currentWaypoint = _currentWaypoint._previousWaypoint;
                    }
                    else
                    {
                        _currentWaypoint = _currentWaypoint._nextWaypoint;
                        _direction = 0;
                    }
                }
            }

            _controller.SetDestination(_currentWaypoint.GetPosition(), _currentWaypoint.name);
        }
    }
}
