using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    private const float spawnFrequency = 3f;
    private float spawnTimer;
    private static List<Food> foods;

    public static List<Food> Foods
    {
        get { return foods; }
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnFrequency;
        foods = new List<Food>();
    }

    void SpawnFood()
    {
        if (spawnTimer <= 0)
        {
            Food newFood = new Food();
            foods.Add(newFood);
            spawnTimer = spawnFrequency;
        }
        spawnTimer -= Time.deltaTime;
    }

    void RemoveFood()
    {
        var foodsToRemove = new List<Food>();
        foreach (Food f in foods)
        {
            if (!f.FoodGameObject.activeSelf)
                foodsToRemove.Add(f);
        }

        foreach (var foodToRemove in foodsToRemove)
        {
            foods.Remove(foodToRemove);
        }
    }
    // Update is called once per frame
    void Update()
    {
        SpawnFood();
        RemoveFood();
    }
}
