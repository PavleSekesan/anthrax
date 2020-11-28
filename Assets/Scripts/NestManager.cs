using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestManager : MonoBehaviour
{
    private static List<Nest> nests;
    public static List<Nest> Nests
    {
        get { return nests; }
    }

    public static void SpawnNest(Vector2 location, Player player)
    {
        Nest newNest = new Nest(location, player);
        nests.Add(newNest);
    }

    // Start is called before the first frame update
    void Start()
    {
        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        nests = new List<Nest>();
        nests.Add(new Nest(new Vector2(-cameraWidth / 4, 0), PlayerManager.Players[0]));
        nests.Add(new Nest(new Vector2(cameraWidth / 4, 0), PlayerManager.Players[1]));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
