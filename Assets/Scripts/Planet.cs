using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Planet
{ 
    public GameObject PlanetPrefab;
    public PlanetManager.Planets PlanetType;
    [HideInInspector]
    public int Rating;
}
