using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAnt : Ant
{
    public const int foodCost = 3;
    private const int sensorRange = 50;
    private const float changeDirectionFrequency = 1f;
    private const float nestConstructionTime = 10;
    private float changeDirectionTimer;
    private float nestConstructionTimer;
    private string state = "random";
    private Food foodToRunTo;
    private Vector2 buildingSiteLocation = Vector2.negativeInfinity;
    private float xComponent;
    private float yComponent;
    private GameObject workerAntGameObject;
    
    public GameObject WorkerAntGameObject
    {
        get { return workerAntGameObject; }
    }
    public WorkerAnt(Nest nest) : base(nest) 
    {
        speed = 100;
        changeDirectionTimer = 0;
        nestConstructionTimer = nestConstructionTime;

        workerAntGameObject = new GameObject("Worker Ant");
        workerAntGameObject.transform.position = position;
        var spriteRenderer = workerAntGameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/ant");
        spriteRenderer.sortingOrder = 2;
    }

    private void MoveToPosition(Vector2 position)
    {
        float distance = Vector2.Distance(position, workerAntGameObject.transform.position);
        float dX = position.x - workerAntGameObject.transform.position.x;
        float dY = position.y - workerAntGameObject.transform.position.y;

        xComponent = dX / distance;
        yComponent = dY / distance;
    }

    private void CheckForFood()
    {
        var foods = FoodManager.Foods;

        foreach (var food in foods)
        {
            float foodDistance = Vector2.Distance(food.Position, workerAntGameObject.transform.position);
            if (state == "random" && foodDistance < sensorRange)
            {
                MoveToPosition(food.Position);
                foodToRunTo = food;
                state = "food";
            }
        }

        float nestDistance = Vector2.Distance(nest.Position, workerAntGameObject.transform.position);
        if (state == "food" && Vector2.Distance(foodToRunTo.Position, workerAntGameObject.transform.position) < Map.interactRange)
        {
            MoveToPosition(nest.Position);

            foodToRunTo.DestroyGameObject();
            foodToRunTo = null;
            state = "nest";
        }
        if (state == "nest" && nestDistance < Map.interactRange)
        {
            nest.Player.IncrementFoodAcquired();
            state = "random";
        }
    }

    private void RunToFood()
    {
        if (Vector2.Distance(foodToRunTo.Position, workerAntGameObject.transform.position) < Map.interactRange)
        {
            MoveToPosition(nest.Position);
            foodToRunTo.DestroyGameObject();
            foodToRunTo = null;
            state = "nest";
        }
    }

    private void RunToNest()
    {
        float nestDistance = Vector2.Distance(nest.Position, workerAntGameObject.transform.position);
        if (nestDistance < Map.interactRange)
        {
            nest.Player.IncrementFoodAcquired();
            state = "random";
        }
    }

    private void RunToBuildingSite()
    {
        float buildingSiteDistance = Vector2.Distance(buildingSiteLocation, workerAntGameObject.transform.position);
        if (buildingSiteDistance < Map.interactRange)
        {
            // Stop
            xComponent = 0;
            yComponent = 0;
            state = "construct";
        }
    }

    private void ConstructNest()
    {
        if (nestConstructionTimer <= 0)
        {
            nestConstructionTimer = nestConstructionTime;
            NestManager.SpawnNest(buildingSiteLocation, nest.Player);
            Map.Refresh();
            MoveToPosition(nest.Position);
            state = "nest";
        }
        else
        {
            nestConstructionTimer -= Time.deltaTime;
        }
    }

    private void MoveRandomly()
    {
        if (changeDirectionTimer <= 0)
        {
            xComponent = Random.Range(-1f, 1f);
            yComponent = Mathf.Sqrt(1 - xComponent * xComponent);
            if (Random.Range(0, 2) == 0) yComponent = -yComponent;
            changeDirectionTimer = changeDirectionFrequency;
        }
        else
        {
            changeDirectionTimer -= Time.deltaTime;
        }
    }

    private void CheckCollisions()
    {
        // Check for collision with screen bounds
        if (workerAntGameObject.transform.position.x < -Map.cameraWidth / 2)
            xComponent = Mathf.Abs(xComponent);
        if (workerAntGameObject.transform.position.x > Map.cameraWidth / 2)
            xComponent = -Mathf.Abs(xComponent);
        if (workerAntGameObject.transform.position.y < -Map.cameraHeight / 2)
            yComponent = Mathf.Abs(yComponent);
        if (workerAntGameObject.transform.position.y > Map.cameraHeight / 2)
            yComponent = -Mathf.Abs(yComponent);
    }

    public void OrderNestBuild(Vector2 location)
    {
        state = "build";
        buildingSiteLocation = location;
        MoveToPosition(location);
    }
    public override void Move()
    {
        if (state == "random")
        {
            MoveRandomly();
            CheckForFood();
        }
        else if (state == "food")
        {
            RunToFood();
        }
        else if (state == "nest")
        {
            RunToNest();
        }
        else if (state == "build")
        {
            RunToBuildingSite();
        }
        else if (state == "construct")
        {
            ConstructNest();
        }

        CheckCollisions();

        Vector3 movementUnitVector = new Vector2(xComponent, yComponent);
        workerAntGameObject.transform.position += movementUnitVector * Time.deltaTime * speed;
        position = workerAntGameObject.transform.position;
    }
}
