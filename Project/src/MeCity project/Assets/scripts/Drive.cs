using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{
    public List<GameObject> waypoints;
    private GameObject currentWaypoint;
    private int waypointCounter = 0;

    // script used to "drive" the car through the waypoints list
    public void Start()
    {
        currentWaypoint = waypoints[waypointCounter];
    }
    void Update()
    {
        // check if car's position is the same as the waypoint position
        if ((int)transform.position.x*100 == (int)currentWaypoint.transform.position.x*100 && (int)transform.position.z*100 == (int)currentWaypoint.transform.position.z*100)
        {
            // get the next waypoint
            if (waypointCounter < waypoints.Count - 1)
            {
                waypointCounter++;
            }
            else
            {
                waypointCounter = 0;
            }
            currentWaypoint = waypoints[waypointCounter];
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, currentWaypoint.transform.position - transform.position, 10f, 0.0f));
            transform.Rotate(Vector3.up, 180f);
        }
        else
        // move the car to the waypoint
        {
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.transform.position, 15f * Time.deltaTime);
        }
    }
}