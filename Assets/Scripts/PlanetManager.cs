using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlanetManager : BasicManager<PlanetManager>
{
    List<GameObject> _cells = new List<GameObject>();

    [SerializeField]
    List<Planet> _planets = new List<Planet>();

    List<GameObject> _planetsObjects = new List<GameObject>();

    System.Random _rand;

    // planets are 30% of cells
    [SerializeField]
    float _planetIndex = 0.3f;

    [HideInInspector]
    public enum Planets
    {
        None,
        Saturn,
        Jupiter,
        Mars
    }

    public void PlacePlanets(List<GameObject> cells, Vector2 size)
    {
        _cells = cells;

        int planetCount = (int)(_cells.Count * _planetIndex);
        //int testCount = 0;
        Debug.Log("planets count is " + planetCount);

        // get random planet index
        _rand = new System.Random();
        for (int i = 0; planetCount > 0; ++i)
        {
            int r = _rand.Next(0, _cells.Count - i);
            if (r < planetCount)
            {
                GenerateAndInstantiatePlanet(i, size);
                planetCount--;
            }
        }
    }

    void GenerateAndInstantiatePlanet(int i, Vector2 size)
    {
        // get random planet from list
        int planetCount = Enum.GetNames(typeof(Planets)).Length;
        int planetIndex = _rand.Next(1, planetCount);
        //Debug.LogError(planetIndex);

        Transform cell = _cells[i].transform;
        Planet planet = _planets.FirstOrDefault(pl => planetIndex == (int)pl.PlanetType);
        if (planet != null)
        {
            GameObject planetPrefab = Instantiate(planet.PlanetPrefab, cell);
            cell.GetComponent<CellBase>().HasPlanet = true;
            planetPrefab.transform.localScale = new Vector3(size.y, transform.localScale.y, size.x);
            _planetsObjects.Add(planetPrefab);
        }
        else
            Debug.LogError("something is wrong");

    }
    public void GeneratePlanetRatings()
    {
        foreach (var planet in _planetsObjects)
        {
            int planetIndex = _rand.Next(1, _planetsObjects.Count);
            //Debug.LogError(planetIndex);
            Transform cell = planet.transform.parent;
            CanvasManager.instance.CreateText(cell, planetIndex.ToString());
        }
    }
}
