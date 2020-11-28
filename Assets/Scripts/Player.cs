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
    private string buildNestKey;
    private string cursorUpKey;
    private string cursorDownKey;
    private string cursorLeftKey;
    private string cursorRightKey;

    public Color Color
    {
        get { return color; }
    }
    public Nest SelectedNest
    {
        get { return selectedNest; }
    }

    public int FoodAcquired
    {
        get { return foodAcquired; }
    }
    public string SpawnAntKey
    {
        get { return spawnAntKey; }
    }
    public string CycleSelectedNestKey
    {
        get { return cycleSelectedNestKey; }
    }
    public string BuildNestKey
    {
        get { return buildNestKey; }
    }
    public string CursorUpKey
    {
        get { return cursorUpKey; }
    }
    public string CursorDownKey
    {
        get { return cursorDownKey; }
    }
    public string CursorLeftKey
    {
        get { return cursorLeftKey; }
    }
    public string CursorRightKey
    {
        get { return cursorRightKey; }
    }

    public Player(Color color, string spawnAntKey, string cycleSelectedNestKey, string buildNestKey,
        string cursorUpKey, string cursorDownKey, string cursorLeftKey, string cursorRightKey)
    {
        this.color= color;
        selectedNest = null;
        foodAcquired = 0;

        this.spawnAntKey = spawnAntKey;
        this.cycleSelectedNestKey = cycleSelectedNestKey;
        this.buildNestKey = buildNestKey;
        this.cursorUpKey = cursorUpKey;
        this.cursorDownKey = cursorDownKey;
        this.cursorLeftKey = cursorLeftKey;
        this.cursorRightKey = cursorRightKey;
    }

    public void SelectNest(Nest n)
    {
        selectedNest = n;
    }

    public void IncrementFoodAcquired(int increment = 1)
    {
        if (foodAcquired + increment >= 0)
            foodAcquired += increment;
    }
}
