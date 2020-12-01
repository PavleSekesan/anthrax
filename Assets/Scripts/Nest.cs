using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Nest
{
    public const int foodCost = 10;
    public const int territoryDiameter = 400;
    private Vector2 position;
    private Sprite sprite;
    private Player player;
    private GameObject nestObject;

    public Vector2 Position
    {
        get { return position; }
    }

    public Player Player
    {
        get { return player; }
    }

    public Nest(Vector2 position, Player player)
    {
        this.position = position;
        this.player = player;

        nestObject = new GameObject("Nest");
        nestObject.transform.position = position;
        var spriteRenderer = nestObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/nest");
        spriteRenderer.sortingOrder = -1;
    }

    public void changeToSelected()
    {
        var spriteRendered = nestObject.GetComponent<SpriteRenderer>();
        spriteRendered.sprite = Resources.Load<Sprite>("Sprites/nestSelected");
    }

    public void changeToDeselected()
    {
        var spriteRendered = nestObject.GetComponent<SpriteRenderer>();
        spriteRendered.sprite = Resources.Load<Sprite>("Sprites/nest");
    }
}
