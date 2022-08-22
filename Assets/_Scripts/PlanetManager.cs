using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlanetFactory))]
public class PlanetManager : MonoBehaviour
{
    public static PlanetManager instance { get; private set; }
    public Planet[] initialPlanets;
    public float G = 10;
    public float dis = 10;
    public GameMode currentMode = GameMode.Edit;
    public Transform camera;
    public int cloestPlanetToCamera = 0;
    public UnityEvent<GameMode> OnGameModeChange;
    public UnityEvent<Planet> OnFocusPlanetChanged;

    private List<(int, int)> pairs;
    private List<Planet> planets = new List<Planet>();
    private bool shouldCalculateForce;
    private PlanetFactory factory;

    public void CreatePlanet(Vector3 position, Quaternion rotation)
    {
        planets.Add(factory.CreatePlanet(position, rotation));
        calculatePairs();
    }

    public void ToggleMode()
    {
        currentMode = currentMode == GameMode.Simulation ? GameMode.Edit : GameMode.Simulation;
        OnGameModeChange.Invoke(currentMode);
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
        camera = Camera.main.transform;
        foreach (var p in initialPlanets)
        {
            planets.Add(p);
            p.GetComponent<PlanetGen>().GeneratePlanet();
            p.Init(1);
        }
        calculatePairs();
        OnGameModeChange.AddListener(HandleGameModeChange);
        HandleGameModeChange(currentMode);
        factory = GetComponent<PlanetFactory>();
    }

    private void Update()
    {
        if (shouldCalculateForce)
        {
            CalculateForce();
        }
        else
        {
            FindNearestPlanetToCamera();
        }
    }

    private void HandleGameModeChange(GameMode mode)
    {
        if(mode == GameMode.Simulation)
        {
            Time.timeScale = 1;
            shouldCalculateForce = true;
        }
        else
        {
            Time.timeScale = 0;
            shouldCalculateForce = false;
        }
    }

    private void FindNearestPlanetToCamera() {
        int prevPlanet = cloestPlanetToCamera;
        float cloestDistance = int.MaxValue;
        for (int i = 0; i < planets.Count; i++)
        {
            float dist = Vector3.Distance(camera.position, planets[i].transform.position);
            if (dist < cloestDistance)
            {
                cloestDistance = dist;
                cloestPlanetToCamera = planets[i].id;
            }
            
        }
        if(cloestPlanetToCamera != prevPlanet)
        {
            OnFocusPlanetChanged.Invoke(planets[cloestPlanetToCamera]);
        }
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
            //Debug.Log($"force for {p1.id},{p2.id} is {force}");
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
                //Debug.Log($"init struct is for {pairs[pairsIndex]}");
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

public enum GameMode
{
    Simulation, Edit
}