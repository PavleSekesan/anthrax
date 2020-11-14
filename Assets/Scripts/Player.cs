using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public Color color;
    public Nest selectedNest;
    public List<Territory> ownedTerritories;

    public Player(Color color, Territory startingTerritory)
    {
        this.color = color;
        ownedTerritories = new List<Territory>() { startingTerritory };
        selectedNest = startingTerritory.GetNest();
    }

    public Color GetColor()
    {
        return color;
    }

    public Nest GetSelectedNest()
    {
        return selectedNest;
    }
    
    public List<Territory> GetTerritories()
    {
        return ownedTerritories;
    }

    public void AddTeritory(Territory t)
    {
        ownedTerritories.Add(t);
    }
}
