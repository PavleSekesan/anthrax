using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntManager : MonoBehaviour
{
    private static List<Ant> ants;

    public static void SpawnAnt(Player player)
    {
        WorkerAnt newWorkerAnt = new WorkerAnt(player.SelectedNest);
        ants.Add(newWorkerAnt);
    }
    // Start is called before the first frame update
    void Start()
    {
        ants = new List<Ant>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move
        foreach (var ant in ants)
            ant.Move();
    }
}
