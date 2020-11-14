using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAnt : Ant
{
    private const int sensorRange = 50;
    private const int frameLoopback = 120;
    private int frameCounter = 0;
    private float xComponent;
    private float yComponent;
    GameObject workerAnt;
    
    public WorkerAnt(Nest nest) : base(nest) 
    {
        speed = 100;

        workerAnt = new GameObject("Worker Ant");
        workerAnt.transform.position = position;
        var spriteRenderer = workerAnt.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/ant");
        spriteRenderer.sortingOrder = 2;
    }
    public override void Move()
    {
        if (frameCounter == 0)
        {
            xComponent = Random.Range(-1f, 1f);
            yComponent = Mathf.Sqrt(1 - xComponent * xComponent);
            if (Random.Range(0, 2) == 0) yComponent = -yComponent;
            
        }

        // Check for collision with screen bounds
        if (workerAnt.transform.position.x < -Map.cameraWidth / 2)
            xComponent = Mathf.Abs(xComponent);
        if (workerAnt.transform.position.x > Map.cameraWidth / 2)
            xComponent = -Mathf.Abs(xComponent);
        if (workerAnt.transform.position.y < -Map.cameraHeight / 2)
            yComponent = Mathf.Abs(yComponent);
        if (workerAnt.transform.position.y > Map.cameraHeight / 2)
            yComponent = -Mathf.Abs(yComponent);

        Vector3 movementUnitVector = new Vector2(xComponent, yComponent);
        workerAnt.transform.position += movementUnitVector * Time.deltaTime * speed;

        frameCounter = (frameCounter + 1) % frameLoopback;
    }
}
