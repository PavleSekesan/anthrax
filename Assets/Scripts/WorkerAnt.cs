using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAnt : Ant
{
    public static int FoodCost = 3;
    private float nestConstructionTime;
    private float nestConstructionTimer;
    private Food foodToRunTo;
    private Vector2 buildingSiteLocation = Vector2.negativeInfinity;

    public WorkerAnt(Nest nest) : base(nest) 
    {
        speed = 100;
        health = 20;
        sensorRange = 50;
        state = "random";
        nestConstructionTime = 10f;
        nestConstructionTimer = nestConstructionTime;
        
        antGameObject = new GameObject("Worker Ant");
        antGameObject.transform.position = position;
        var spriteRenderer = antGameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/workerAnt");
        spriteRenderer.sortingOrder = 2;
    }

    protected override void CheckCollisionWithTerritory()
    {
        bool collide = true;
        foreach (Nest nest in NestManager.Nests)
        {
            Vector2 antPosition = antGameObject.transform.position;
            Vector2 nestPosition = nest.Position;
            float distanceToNest = Vector2.Distance(antPosition, nestPosition);
            if (nest.Player == this.nest.Player && distanceToNest < Nest.territoryDiameter)
            {
                collide = false;
            }
            else if (nest.Player != this.nest.Player && distanceToNest < Nest.territoryDiameter)
            {
                collide = true;
            }
        }

        if (collide)
        {
            xComponent = -xComponent;
            yComponent = -yComponent;
        }
    }


    private void CheckForFood()
    {
        var foods = FoodManager.Foods;

        foreach (var food in foods)
        {
            float foodDistance = Vector2.Distance(food.Position, antGameObject.transform.position);
            if (foodDistance < sensorRange)
            {
                MoveToPosition(food.Position);
                foodToRunTo = food;
                state = "food";
            }
        }
    }

    private void RunToFood()
    {
        if (Vector2.Distance(foodToRunTo.Position, antGameObject.transform.position) < Map.interactRange)
        {
            MoveToPosition(nest.Position);
            foodToRunTo.DestroyGameObject();
            foodToRunTo = null;
            state = "nest";
        }
    }

    private void RunToNest()
    {
        float nestDistance = Vector2.Distance(nest.Position, antGameObject.transform.position);
        if (nestDistance < Map.interactRange)
        {
            nest.Player.IncrementFoodAcquired();
            state = "random";
        }
    }

    private void RunToBuildingSite()
    {
        float buildingSiteDistance = Vector2.Distance(buildingSiteLocation, antGameObject.transform.position);
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

    public void OrderNestBuild(Vector2 location)
    {
        bool outOfBounds = true;
        foreach (Nest nest in NestManager.Nests)
        {
            Vector2 nestPosition = nest.Position;
            float distanceToNest = Vector2.Distance(location, nestPosition);
            if (nest.Player == this.nest.Player && distanceToNest < Nest.territoryDiameter)
            {
                outOfBounds = false;
            }
            else if (nest.Player != this.nest.Player && distanceToNest < Nest.territoryDiameter)
            {
                outOfBounds = true;
            }
        }

        if (!outOfBounds)
        {
            state = "build";
            buildingSiteLocation = location;
            MoveToPosition(location);
        }
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
        CheckCollisionWithTerritory();

        Vector3 movementUnitVector = new Vector2(xComponent, yComponent);
        antGameObject.transform.position += movementUnitVector * Time.deltaTime * speed;
        position = antGameObject.transform.position;
    }
}
