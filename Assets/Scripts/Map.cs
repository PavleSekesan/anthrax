using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static float cameraWidth;
    public static float cameraHeight;
    public static float interactRange = 2;
    void Start()
    {
        // Get width and height in world units
        cameraHeight = Camera.main.orthographicSize * 2;
        cameraWidth = cameraHeight * Camera.main.aspect;

        DrawMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Player ClosestPlayer(Vector2 pixel)
    {
        Vector2 pixelPosition = new Vector2((float)(-cameraWidth / 2 + pixel.x), (float)(cameraHeight / 2 - pixel.y)); 

        float minDistance = float.PositiveInfinity;
        Player closestPlayer = null;

        foreach (Nest nest in NestManager.Nests)
        {
            float nestDistanceToPixel = Vector2.Distance(pixelPosition, nest.Position);

            if (nestDistanceToPixel < minDistance)
            {
                minDistance = nestDistanceToPixel;
                closestPlayer = nest.Player;
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
                boundaryTexture.SetPixel(x, y, closestPlayer.Color);
            }
        }
        boundaryTexture.filterMode = FilterMode.Bilinear;
        boundaryTexture.Apply();
        GetComponent<SpriteRenderer>().sprite = Sprite.Create(boundaryTexture, new Rect(0, 0, textureWidth, textureHeight), Vector2.one * 0.5f, 1);
    }
}
