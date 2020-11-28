using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntManager : MonoBehaviour
{
    private static List<Ant> ants;
    private const int startingAntNumber = 5;
    public static void SpawnAnt(Player player)
    {
        WorkerAnt newWorkerAnt = new WorkerAnt(player.SelectedNest);
        ants.Add(newWorkerAnt);
    }

    public static void SpawnAnt(Nest nest)
    {
        WorkerAnt newWorkerAnt = new WorkerAnt(nest);
        ants.Add(newWorkerAnt);
    }

    public static void BuildNest(Player player)
    {
        // Find player cursor position
        Cursor playerCursor = null;
        foreach (Cursor cursor in CursorManager.Cursors)
        {
            if (cursor.Player == player)
            {
                playerCursor = cursor;
                break;
            }
        }
        Vector2 cursorPosition = playerCursor.CursorGameObject.transform.position;

        WorkerAnt closestWorkerAnt = null;
        float minDistance = float.MaxValue;
        foreach(WorkerAnt workerAnt in ants)
        {
            if (workerAnt.Nest.Player == player)
            {
                Vector2 workerAntPosition = workerAnt.WorkerAntGameObject.transform.position;
                float distanceToCursor = Vector2.Distance(workerAntPosition, cursorPosition);
                if (distanceToCursor < minDistance)
                {
                    minDistance = distanceToCursor;
                    closestWorkerAnt = workerAnt;
                }
            }
        }

        closestWorkerAnt.OrderNestBuild(cursorPosition);
    }
    // Start is called before the first frame update
    void Start()
    {
        ants = new List<Ant>();
        foreach (Nest nest in NestManager.Nests)
        {
            for (int i = 0; i < startingAntNumber; i++)
            {
                SpawnAnt(nest);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move
        foreach (var ant in ants)
            ant.Move();
    }
}
