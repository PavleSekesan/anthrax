using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Territory
{
    private Nest nest;

    public Territory(Vector2 nestPosition)
    {
        nest = new Nest(nestPosition);
    }

    public Nest GetNest()
    {
        return nest;
    }
}
