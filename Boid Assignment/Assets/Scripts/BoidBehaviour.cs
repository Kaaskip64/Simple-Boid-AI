using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Most of this script is implemented from this tutorial: https://www.dawn-studio.de/tutorials/boids/
public class BoidBehaviour : MonoBehaviour
{
    public int swarmIndex { get; set; }
    public float noClumpingRadius = 5f;
    public float localAreaRadius = 10f;
    public float speed = 10f;
    public float steeringSpeed = 100f;

    public void SimulateMovement(List<BoidBehaviour> other, float time)
    {
        var steering = Vector3.zero;
        var alignmentDirection = Vector3.zero;
        var alignmentCount = 0;
        var cohesionDirection = Vector3.zero;
        var cohesionCount = 0;

        int separationCount = 0;

        transform.position += transform.TransformDirection(new Vector3(0, 0, speed)) * time;

        Vector3 separationDirection = Vector3.zero;

        foreach (BoidBehaviour boid in other)
        {
            if (boid == this)
            {
                continue;
            }
            var distance = Vector3.Distance(boid.transform.position, transform.position);

            if (distance < noClumpingRadius)
            {
                separationDirection += boid.transform.position - transform.position;
                separationCount++;
            }

            if (distance < localAreaRadius)
            {
                alignmentDirection += boid.transform.forward;
                alignmentCount++;

                cohesionDirection += boid.transform.position - transform.position;
                cohesionCount++;
            }
        }

        cohesionDirection -= transform.position;

        if (separationCount > 0)
        {
            separationDirection /= separationCount;
        }
        separationDirection = -separationDirection.normalized;

        steering = separationDirection;

        steering += alignmentDirection;

        steering += cohesionDirection;

        if (steering != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(steering), steeringSpeed * time);
        }

    }
}
