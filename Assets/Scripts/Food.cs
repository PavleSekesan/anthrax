using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food
{
    public Vector2 position;
    public GameObject foodGameObject;

    public Food()
    {
        float positionX = Random.Range(-Map.cameraWidth / 2, Map.cameraWidth / 2);
        float positionY = Random.Range(-Map.cameraHeight / 2, Map.cameraHeight/ 2);

        position = new Vector2(positionX, positionY);

        foodGameObject = new GameObject("Food");
        foodGameObject.transform.position = position;
        var spriteRenderer = foodGameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/food");
        spriteRenderer.sortingOrder = 0;
    }
}
