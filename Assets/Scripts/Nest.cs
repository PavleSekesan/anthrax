using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Nest
{
    private Vector2 position;
    private Sprite sprite;

    public Nest(Vector2 position)
    {
        this.position = position;

        GameObject nestObject = new GameObject("Nest");
        nestObject.transform.position = position;
        var spriteRenderer = nestObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/nest");
        spriteRenderer.sortingOrder = -1;
    }

    public Vector2 GetPosition()
    {
        return position;
    }
}
