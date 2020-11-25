using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food
{
    private Vector2 position;
    private GameObject foodGameObject;

    public Vector2 Position
    {
        get { return position; }
    }

    public GameObject FoodGameObject
    {
        get { return foodGameObject; }
    }

    public void DestroyGameObject()
    {
        foodGameObject.SetActive(false);
    }
    public Food()
    {
        // Spawn on a random position
        float positionX = Random.Range(-Map.cameraWidth / 2, Map.cameraWidth / 2);
        float positionY = Random.Range(-Map.cameraHeight / 2, Map.cameraHeight/ 2);
        position = new Vector2(positionX, positionY);

        // Draw to scene
        foodGameObject = new GameObject("Food");
        foodGameObject.transform.position = position;
        var spriteRenderer = foodGameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/food");
        spriteRenderer.sortingOrder = 0;
    }
}
