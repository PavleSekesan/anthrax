using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private static List<Cursor> cursors;

    public static List<Cursor> Cursors
    {
        get { return cursors; }
    }
    // Start is called before the first frame update
    void Start()
    {
        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        cursors = new List<Cursor>();
        cursors.Add(new Cursor(PlayerManager.Players[0], new Vector2(-cameraWidth / 4, 0)));
        cursors.Add(new Cursor(PlayerManager.Players[1], new Vector2(cameraWidth / 4, 0)));
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Cursor cursor in cursors)
        {
            cursor.Move();
        }
    }
}
