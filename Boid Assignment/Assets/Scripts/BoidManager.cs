using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Most of this script is implemented from this tutorial: https://www.dawn-studio.de/tutorials/boids/
public class BoidManager : MonoBehaviour
{
    public BoidBehaviour boidPrefab;
    public int spawnboids = 20;
    private List<BoidBehaviour> boids;

    void Start()
    {
        boids = new List<BoidBehaviour>();

        for (int i = 0; i < spawnboids; i++)
        {
            SpawnBoid(boidPrefab.gameObject, 0);
        }
    }

    void Update()
    {
        foreach (BoidBehaviour boid in boids)
        {
            boid.SimulateMovement(boids, Time.deltaTime);
        }
    }

    private void SpawnBoid (GameObject prefab, int swarmIndex)
    {
        var boidInstance = Instantiate(prefab);
        boidInstance.transform.localPosition += new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));

        var boidBehaviour = boidInstance.GetComponent<BoidBehaviour>();
        boids.Add(boidBehaviour);
    }
}
