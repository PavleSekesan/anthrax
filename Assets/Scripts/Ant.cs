using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Ant
{
    public Nest nest;
    public Vector2 position;
    public float speed;

    public Ant(Nest nest)
    {
        this.nest = nest;
        position = nest.GetPosition();
    }

    public abstract void Move();
}
