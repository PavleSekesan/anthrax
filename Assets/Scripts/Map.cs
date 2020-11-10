using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Map : MonoBehaviour
{
    public float cameraWidth;
    public float cameraHeight;
    private List<Territory> territories;
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
            new Territory(new Vector2(-cameraWidth / 4, 0), 1, Color.green),
            new Territory(new Vector2(cameraWidth / 4, 0), 2, Color.blue)
        };
        this.territories = territories;
        DrawMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int ClosestTerritoryIndex(Vector2 pixel)
    {
        Vector2 pixelPos = new Vector2((float)(-cameraWidth / 2 + pixel.x), (float)(cameraHeight / 2 - pixel.y)); 

        float minDistance = float.PositiveInfinity;
        int closestTerritoryIndex = -1;

        for (int i = 0; i < territories.Count; i++)
        {
            Vector2 currentSeed = territories[i].GetNest().GetPosition();
            float currentTerritoryDistance = Vector2.Distance(pixelPos, currentSeed);
            
            if (currentTerritoryDistance < minDistance)
            {
                minDistance = currentTerritoryDistance;
                closestTerritoryIndex = i;
            }
        }

        return closestTerritoryIndex;
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
                Territory closestTerritory = territories[ClosestTerritoryIndex(pixel)];
                boundaryTexture.SetPixel(x, y, closestTerritory.GetColor());
            }
        }
        boundaryTexture.filterMode = FilterMode.Bilinear;
        boundaryTexture.Apply();
        GetComponent<SpriteRenderer>().sprite = Sprite.Create(boundaryTexture, new Rect(0, 0, textureWidth, textureHeight), Vector2.one * 0.5f, 1);
    }
}
