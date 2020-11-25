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
        players.Add(new Player(Color.green, "space"));
        players.Add(new Player(Color.blue, "enter"));
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < players.Count; i++)
        {
            Player player = players[i];
            if (player.SelectedNest == null)
                player.SelectNest(NestManager.Nests[i]);
        }
    }
}
