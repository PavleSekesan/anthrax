using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor
{
    private const float cursorSpeed = 500;
    private Player player;
    private GameObject cursorGameObject;

    public Player Player
    {
        get { return player; }
    }

    public GameObject CursorGameObject
    {
        get { return cursorGameObject; }
    }
    
    public Cursor(Player player, Vector2 startingPosition)
    {
        this.player = player;
        cursorGameObject = new GameObject("Cursor");
        cursorGameObject.transform.position = startingPosition;
        var spriteRenderer = cursorGameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/cursor");
        spriteRenderer.sortingOrder = 10;
    }

    public void Move()
    {
        float dx = 0, dy = 0;
        if(Input.GetKey(player.CursorUpKey))
            dy = 1;
        if (Input.GetKey(player.CursorDownKey))
            dy = -1;
        if (Input.GetKey(player.CursorLeftKey))
            dx = -1;
        if (Input.GetKey(player.CursorRightKey))
            dx = 1;

        Vector3 movementVector = new Vector2(dx, dy);
        cursorGameObject.transform.position += movementVector * Time.deltaTime * cursorSpeed;
    }
}
