using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spline : MonoBehaviour {
    //public Transform[] waypoints ;
    //public int rate = 20;
    //private int currentWaypoint = 0;

    public void OnDrawGizmos(){
        //iTween.DrawLine(waypoints, Color.blue);
    }

    public void Start(){
        moveToWaypoint();
    }

    public void moveToWaypoint(){
        //Time = Distance / Rate:
        // var travelTime = Vector3.Distance(transform.position, waypoints[currentWaypoint].position)/rate;

        // //iTween:
        // iTween.MoveTo(gameObject,
        // {"position":waypoints[currentWaypoint],
        // "time":travelTime,
        // "easetype":"linear",
        // "oncomplete":"moveToWaypoint",
        // "Looktarget":waypoints[currentWaypoint].position,
        // "looktime":.4
        // });

        // //Move to next waypoint:
        // currentWaypoint++;
        // if(currentWaypoint>waypoints.Length-2){
        //     currentWaypoint=0;
        // }
    }
}
