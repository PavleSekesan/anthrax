using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static float cameraWidth;
    public static float cameraHeight;
    private List<Player> players;
    private List<Ant> ants = new List<Ant>();
    void Start()
    {
        // Get width and height in world units
        cameraHeight = Camera.main.orthographicSize * 2;
        cameraWidth = cameraHeight * Camera.main.aspect;

        // Initialize starting territory positions for each player fairly
        // (each player has the same starting area)

        List<Territory> territories = new List<Territory>
        {
            // NOTE: currently only two players, algorithm for this placement for n players should be implemented later
            new Territory(new Vector2(-cameraWidth / 4, 0)),
            new Territory(new Vector2(cameraWidth / 4, 0))
        };

        
        this.players = new List<Player>()
        {
            new Player(Color.green, territories[0]),
            new Player(Color.blue, territories[1])
        };

        DrawMap();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            WorkerAnt newWorkerAnt = new WorkerAnt(players[0].GetSelectedNest());
            ants.Add(newWorkerAnt);
        }

        foreach (var ant in ants)
            ant.Move();
    }

    private Player ClosestPlayer(Vector2 pixel)
    {
        Vector2 pixelPos = new Vector2((float)(-cameraWidth / 2 + pixel.x), (float)(cameraHeight / 2 - pixel.y)); 

        float minDistance = float.PositiveInfinity;
        Player closestPlayer = null;

        for (int i = 0; i < players.Count; i++)
        {
            var currentPlayerTerritories = players[i].GetTerritories();
            for (int j = 0; j < currentPlayerTerritories.Count; j++)
            {
                Vector2 currentSeed = currentPlayerTerritories[j].GetNest().GetPosition();
                float currentTerritoryDistance = Vector2.Distance(pixelPos, currentSeed);

                if (currentTerritoryDistance < minDistance)
                {
                    minDistance = currentTerritoryDistance;
                    closestPlayer = players[i];
                }
            }
        }

        return closestPlayer;
    }

    void DrawMap()
    {
        // Draw territory boundaries as a Voronoi diagram
        int textureWidth = (int)cameraWidth;
        int textureHeight = (int)cameraHeight;
        Texture2D boundaryTexture = new Texture2D(textureWidth, textureHeight);
        for(int y = 0; y < textureHeight; y++)
        {
            for (int x = 0; x < textureWidth; x++)
            {
                Vector2 pixel = new Vector2(x, y);
                Player closestPlayer = ClosestPlayer(pixel);
                boundaryTexture.SetPixel(x, y, closestPlayer.GetColor());
            }
        }
        boundaryTexture.filterMode = FilterMode.Bilinear;
        boundaryTexture.Apply();
        GetComponent<SpriteRenderer>().sprite = Sprite.Create(boundaryTexture, new Rect(0, 0, textureWidth, textureHeight), Vector2.one * 0.5f, 1);
    }
}
