using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendlyPatrolAi : MonoBehaviour
{
    [Header("Waypoints")]
    public List<Transform> wayPoint;
    public int currentWaypointIndex = 0;

    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Patrol movement
        Patrol();
    }

    /// <summary>
    /// Patrol between waypoints in a loop.
    /// </summary>
    private void Patrol()
    {
        if (wayPoint == null || wayPoint.Count == 0) return;

        float distanceToWaypoint = Vector3.Distance(
            wayPoint[currentWaypointIndex].position,
            transform.position
        );

        //Debug.Log($"Current waypoint index: {currentWaypointIndex}, Distance to waypoint: {distanceToWaypoint}");

        if (distanceToWaypoint <= 2f) // When close to the waypoint, move to the next one
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % wayPoint.Count;
            //Debug.Log($"Waypoint reached, moving to next: {currentWaypointIndex}");
        }

        navMeshAgent.SetDestination(wayPoint[currentWaypointIndex].position);

        // Optionally rotate to face the direction of movement
        Vector3 directionToWaypoint = wayPoint[currentWaypointIndex].position - transform.position;
        if (directionToWaypoint.sqrMagnitude > Mathf.Epsilon) // To avoid dividing by zero
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToWaypoint);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // Smooth rotation
        }
    }
}
