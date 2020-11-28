using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private Color color;
    private Nest selectedNest;
    private int foodAcquired;
    private string spawnAntKey;
    private string cycleSelectedNestKey;

    public Color Color
    {
        get { return color; }
    }
    public Nest SelectedNest
    {
        get { return selectedNest; }
    }
    public string SpawnAntKey
    {
        get { return spawnAntKey; }
    }

    public string CycleSelectedNestKey
    {
        get { return cycleSelectedNestKey; }
    }

    public Player(Color color, string spawnAntKey, string cycleSelectedNestKey)
    {
        this.color= color;
        selectedNest = null;
        foodAcquired = 0;
        this.spawnAntKey = spawnAntKey;
        this.cycleSelectedNestKey = cycleSelectedNestKey;
    }

    public void SelectNest(Nest n)
    {
        selectedNest = n;
    }

    public void IncrementFoodAcquired()
    {
        foodAcquired++;
    }
}
