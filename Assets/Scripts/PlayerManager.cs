using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static List<Player> players;
    
    public static List<Player> Players
    {
        get { return players; }
    }
    // Start is called before the first frame update
    void Start()
    {
        players = new List<Player>();
        players.Add(new Player(Color.green, "z", "x", "tab", "c", "w", "s", "a", "d"));
        players.Add(new Player(Color.blue, "i", "o", "right shift", "p", "up", "down", "left", "right"));
    }

    // Update is called once per frame
    void Update()
    {
        // Loop through all players
        for (int i = 0; i < players.Count; i++)
        {
            Player player = players[i];
            if (player.SelectedNest == null)
                player.SelectNest(NestManager.Nests[i]);
            
            // Cycle selected nest
            if (Input.GetKeyDown(player.CycleSelectedNestKey))
            {
                // Change selected nest
                Nest newSelectedNest = player.SelectedNest;
                var allNests = NestManager.Nests;
                bool foundCurrentSelectedNestIndex = false;
                bool foundNewSelectedNest = false;
                for (int nestIndex = 0; !foundNewSelectedNest; nestIndex = (nestIndex + 1) % allNests.Count)
                {
                    Nest currentNest = allNests[nestIndex];
                    if (foundCurrentSelectedNestIndex && currentNest.Player == player)
                    {
                        newSelectedNest = currentNest;
                        foundNewSelectedNest = true;
                    }
                    if(currentNest == player.SelectedNest)
                    {
                        foundCurrentSelectedNestIndex = true;
                    }
                }

                // Change appearance of selected nest
                player.SelectedNest.changeToDeselected();
                newSelectedNest.changeToSelected();

                player.SelectNest(newSelectedNest);
            }

            // Build nest
            int newNestCost = Nest.foodCost;
            if (Input.GetKeyDown(player.BuildNestKey) && player.FoodAcquired >= Nest.foodCost)
            {
                player.IncrementFoodAcquired(-newNestCost);
                AntManager.BuildNest(player);
            }

            // Spawn worker ants
            int workerAntCost = WorkerAnt.FoodCost;
            if (Input.GetKeyDown(player.SpawnWorkerAntKey) && player.FoodAcquired >= workerAntCost)
            {
                player.IncrementFoodAcquired(-workerAntCost);
                AntManager.SpawnWorkerAnt(player);
            }

            // Spawn warrior ants
            int warriorAntCost = WarriorAnt.FoodCost;
            if (Input.GetKeyDown(player.SpawnWarriorAntKey) && player.FoodAcquired >= warriorAntCost)
            {
                player.IncrementFoodAcquired(-warriorAntCost);
                AntManager.SpawnWarriorAnt(player);
            }
        }

    }
}
