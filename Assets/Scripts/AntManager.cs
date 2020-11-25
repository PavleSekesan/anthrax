using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntManager : MonoBehaviour
{
    private List<Ant> ants;

    private void SpawnAnt(Player player)
    {
        if (Input.GetKeyDown(player.SpawnAntKey))
        {
            WorkerAnt newWorkerAnt = new WorkerAnt(player.SelectedNest);
            ants.Add(newWorkerAnt);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ants = new List<Ant>();
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn
        foreach (Player player in PlayerManager.Players)
            SpawnAnt(player);

        // Move
        foreach (var ant in ants)
            ant.Move();
    }
}
