using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    public int nextPlayerTeam;
    private SpawnpointDefenders[] _spawnpointsdefenders;
    private SpawnpointIntruders[] _spawnpointsintruders;
    
    private void Awake()
    {
        Instance = this;
       _spawnpointsintruders = GetComponentsInChildren<SpawnpointIntruders>();
        _spawnpointsdefenders = GetComponentsInChildren<SpawnpointDefenders>();
    }

    public Transform GetSpawnpointDefenders()
    {
        return _spawnpointsdefenders[Random.Range(0, _spawnpointsdefenders.Length)].transform;
    }
    
    public Transform GetSpawnpointIntruders()
    {
        return _spawnpointsintruders[Random.Range(0, _spawnpointsintruders.Length)].transform;
    }

    public void UpdateTeam()
    {
        if (nextPlayerTeam == 1)
        {
            nextPlayerTeam = 2;
        }
        else
        {
            nextPlayerTeam = 1;
        }
    }
}



