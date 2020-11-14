using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAnt : Ant
{
    private const int sensorRange = 50;
    private const int frameLoopback = 120;
    private int frameCounter = 0;
    private bool backToNest = false;
    private bool runToFood = false;
    private Vector2 foodRunPosition = Vector2.negativeInfinity;
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

    private void RunToFood()
    {
        var foods = Map.GetFoods();

        foreach (var food in foods)
        {
            float foodDistance = Vector2.Distance(food.position, workerAnt.transform.position);
            if (!backToNest && !runToFood && foodDistance < sensorRange)
            {
                float dX = food.position.x - workerAnt.transform.position.x;
                float dY = food.position.y - workerAnt.transform.position.y;

                xComponent = dX / foodDistance;
                yComponent = dY / foodDistance;
                foodRunPosition = food.position;
                runToFood = true;
            }
        }


        float nestDistance = Vector2.Distance(nest.GetPosition(), workerAnt.transform.position);
        if (runToFood && Vector2.Distance(foodRunPosition, workerAnt.transform.position) < Map.interactRange)
        {
            float dX = nest.GetPosition().x - workerAnt.transform.position.x;
            float dY = nest.GetPosition().y - workerAnt.transform.position.y;

            xComponent = dX / nestDistance;
            yComponent = dY / nestDistance;
            foodRunPosition = Vector2.negativeInfinity;
            runToFood = false;
            backToNest = true;
        }
        if (backToNest && nestDistance < Map.interactRange)
        {
            backToNest = false;
        }
    }
    public override void Move()
    {
        RunToFood();
        if (frameCounter == 0 && !runToFood && !backToNest)
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
        position = workerAnt.transform.position;

        frameCounter = (frameCounter + 1) % frameLoopback;
    }
}
