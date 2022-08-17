using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public static PlanetManager instance { get; private set; }
    public Planet[] initialPlanets;
    public float G = 10;
    public float dis = 10;

    private List<(int, int)> pairs;
    private List<Planet> planets = new List<Planet>();

    public void AddPlanet(Planet p)
    {
        planets.Add(p);
        calculatePairs();
    }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        foreach (var p in initialPlanets)
        {
            planets.Add(p);
        }
        calculatePairs();
    }

    private void Update()
    {
        CalculateForce();
    }

    private void CalculateForce()
    {
        for (int i = 0; i < pairs.Count; i++)
        {
            Planet p1 = planets[pairs[i].Item1];
            Vector3 pos1 = p1.transform.position;
            Planet p2 = planets[pairs[i].Item2];
            Vector3 pos2 = p2.transform.position;

            float force = (p1.Mass * p2.Mass * G) / Mathf.Pow(Vector3.Distance(pos1, pos2)*dis,2);
            p1.AddForceTo(force, (pos2 - pos1).normalized);
            p2.AddForceTo(force, (pos1 - pos2).normalized);
            Debug.Log($"force for {p1.id},{p2.id} is {force}");
        }
    }

    private void calculatePairs()
    {
        pairs = new List<(int, int)>();
        int pairsIndex = 0;
        for (int i = 0; i < planets.Count; i++)
        {
            planets[i].id = i;
            for (int j = i + 1; j < planets.Count; j++)
            {
                pairs.Add((i, j));
                Debug.Log($"init struct is for {pairs[pairsIndex]}");
                pairsIndex++;
            }
        }
        planets[planets.Count - 1].id = planets.Count - 1;
    }

    private int sumLength(int f)
    {
        if (f == 1)
            return 1;
        else
            return f + sumLength(f - 1);
    }
}
