using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static float cameraWidth;
    public static float cameraHeight;
    public static float interactRange = 2;
    private List<Player> players;
    private List<Ant> ants = new List<Ant>();
    private List<Nest> nests = new List<Nest>();
    private static List<Food> foods = new List<Food>();
    private int foodFrameCounter = 0;
    private int foodFrameLoopback = 600; 
    void Start()
    {
        // Get width and height in world units
        cameraHeight = Camera.main.orthographicSize * 2;
        cameraWidth = cameraHeight * Camera.main.aspect;

        // Initialize starting territory positions for each player fairly
        // (each player has the same starting area)

        players = new List<Player>()
        {
            new Player(Color.green),
            new Player(Color.blue)
        };

        nests.Add(new Nest(new Vector2(-cameraWidth / 4, 0), players[0]));
        nests.Add(new Nest(new Vector2(cameraWidth / 4, 0), players[1]));

        players[0].AddNest(nests[0]);
        players[1].AddNest(nests[1]);

        DrawMap();
    }

    // Update is called once per frame
    void Update()
    {
        // Food
        if (foodFrameCounter == 0)
        {
            Food newFood = new Food();
            foods.Add(newFood);
        }
        foodFrameCounter = (foodFrameCounter + 1) % foodFrameLoopback;

        // Ants
        if (Input.GetKeyDown("space"))
        {
            WorkerAnt newWorkerAnt = new WorkerAnt(players[0].GetSelectedNest());
            ants.Add(newWorkerAnt);
        }

        

        List<Food> foodsToRemove = new List<Food>();
        foreach (var food in foods)
        {
            foreach(var ant in ants)
            {
                if (Vector2.Distance(food.position, ant.position) < interactRange)
                {
                    ant.nest.GetPlayer().foodAcquired++;
                    Destroy(food.foodGameObject);
                    foodsToRemove.Add(food);
                }
            }
        }

        foreach (var ant in ants)
            ant.Move();

        foreach (var foodToRemove in foodsToRemove)
        {
            foods.Remove(foodToRemove);
        }
    }

    private Player ClosestPlayer(Vector2 pixel)
    {
        Vector2 pixelPos = new Vector2((float)(-cameraWidth / 2 + pixel.x), (float)(cameraHeight / 2 - pixel.y)); 

        float minDistance = float.PositiveInfinity;
        Player closestPlayer = null;

        for (int i = 0; i < players.Count; i++)
        {
            var currentPlayerNests = players[i].GetNests();
            for (int j = 0; j < currentPlayerNests.Count; j++)
            {
                Vector2 currentSeed = currentPlayerNests[j].GetPosition();
                float currentNestDistance = Vector2.Distance(pixelPos, currentSeed);

                if (currentNestDistance < minDistance)
                {
                    minDistance = currentNestDistance;
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

    public static List<Food> GetFoods()
    {
        return foods;
    }
}
