using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Territory
{
    private Nest nest;
    private int player;
    private Color color;

    public Territory(Vector2 nestPosition, int player, Color color)
    {
        nest = new Nest(nestPosition);
        this.player = player;
        this.color = color;
    }

    public Nest GetNest()
    {
        return nest;
    }

    public int GetPlayer()
    {
        return player;
    }

    public Color GetColor()
    {
        return color;
    }
}
