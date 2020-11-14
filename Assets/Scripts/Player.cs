using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public Color color;
    public Nest selectedNest;
    public List<Nest> ownedNests;
    public int foodAcquired;

    public Player(Color color)
    {
        this.color = color;
        ownedNests = new List<Nest>();
        foodAcquired = 0;
    }

    public Color GetColor()
    {
        return color;
    }

    public Nest GetSelectedNest()
    {
        return selectedNest;
    }
    
    public List<Nest> GetNests()
    {
        return ownedNests;
    }

    public void AddNest(Nest n)
    {
        ownedNests.Add(n);
        selectedNest = n;
    }
}
