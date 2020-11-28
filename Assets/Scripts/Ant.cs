using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Ant
{
    protected Nest nest;
    protected Vector2 position;
    protected float speed;

    public Nest Nest
    {
        get { return nest; }
    }

    public Ant(Nest nest)
    {
        this.nest = nest;
        position = nest.Position;
    }

    public abstract void Move();
}
