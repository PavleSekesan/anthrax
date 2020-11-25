using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Nest
{
    private Vector2 position;
    private Sprite sprite;
    private Player player;

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

        GameObject nestObject = new GameObject("Nest");
        nestObject.transform.position = position;
        var spriteRenderer = nestObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/nest");
        spriteRenderer.sortingOrder = -1;
    }
}
