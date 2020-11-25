using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAnt : Ant
{
    private const int sensorRange = 50;
    private const float changeDirectionFrequency = 1f;
    private float changeDirectionTimer;
    private bool backToNest = false;
    private bool runToFood = false;
    private Food foodToRunTo;
    private float xComponent;
    private float yComponent;
    private GameObject workerAnt;
    
    public WorkerAnt(Nest nest) : base(nest) 
    {
        speed = 100;
        changeDirectionTimer = 0;

        workerAnt = new GameObject("Worker Ant");
        workerAnt.transform.position = position;
        var spriteRenderer = workerAnt.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/ant");
        spriteRenderer.sortingOrder = 2;
    }

    private void RunToFood()
    {
        var foods = FoodManager.Foods;

        foreach (var food in foods)
        {
            float foodDistance = Vector2.Distance(food.Position, workerAnt.transform.position);
            if (!backToNest && !runToFood && foodDistance < sensorRange)
            {
                float dX = food.Position.x - workerAnt.transform.position.x;
                float dY = food.Position.y - workerAnt.transform.position.y;

                xComponent = dX / foodDistance;
                yComponent = dY / foodDistance;
                foodToRunTo = food;
                runToFood = true;
            }
        }


        float nestDistance = Vector2.Distance(nest.Position, workerAnt.transform.position);
        if (runToFood && Vector2.Distance(foodToRunTo.Position, workerAnt.transform.position) < Map.interactRange)
        {
            float dX = nest.Position.x - workerAnt.transform.position.x;
            float dY = nest.Position.y - workerAnt.transform.position.y;

            xComponent = dX / nestDistance;
            yComponent = dY / nestDistance;

            foodToRunTo.DestroyGameObject();
            foodToRunTo = null;
            runToFood = false;
            backToNest = true;
        }
        if (backToNest && nestDistance < Map.interactRange)
        {
            nest.Player.IncrementFoodAcquired();
            backToNest = false;
        }
    }
    public override void Move()
    {
        RunToFood();
        if (changeDirectionTimer <= 0 && !runToFood && !backToNest)
        {
            xComponent = Random.Range(-1f, 1f);
            yComponent = Mathf.Sqrt(1 - xComponent * xComponent);
            if (Random.Range(0, 2) == 0) yComponent = -yComponent;
            changeDirectionTimer = changeDirectionFrequency;
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

        changeDirectionTimer -= Time.deltaTime;
    }
}
