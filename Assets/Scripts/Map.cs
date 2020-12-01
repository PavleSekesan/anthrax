using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static float cameraWidth;
    public static float cameraHeight;
    public static float interactRange = 2;
    private static bool shouldRefresh = false;
    private int sortingLayer;

    public static void Refresh()
    {
        shouldRefresh = true;
    }
    void Start()
    {
        // Get width and height in world units
        cameraHeight = Camera.main.orthographicSize * 2;
        cameraWidth = cameraHeight * Camera.main.aspect;
        sortingLayer = -1000;

        DrawMap();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldRefresh)
        {
            DrawMap();
            shouldRefresh = false;
        }
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

    private void DrawMap()
    {
        foreach (Nest nest in NestManager.Nests)
        {
            GameObject territory = new GameObject("Territory");
            territory.transform.position = nest.Position;
            var spriteRenderer = territory.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/territory");
            spriteRenderer.color = nest.Player.Color;
            spriteRenderer.sortingOrder = sortingLayer++;
        }
    }
}
