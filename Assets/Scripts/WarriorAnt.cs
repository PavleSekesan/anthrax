using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAnt : Ant
{
    public static int FoodCost = 5;
    private int attackRange;
    private float maxDamage;
    private Ant antToFollow;
    private float attackDelay;
    private float attackTimer;

    public WarriorAnt(Nest nest) : base(nest)
    {
        speed = 150;
        health = 50;
        sensorRange = 50;
        attackRange = 10;
        maxDamage = 50;
        attackDelay = 0.5f;
        attackTimer = attackDelay;
        state = "random";

        antGameObject = new GameObject("Warrior Ant");
        antGameObject.transform.position = position;
        var spriteRenderer = antGameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/warriorAnt");
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
            if (distanceToNest < Nest.territoryDiameter)
            {
                collide = false;
            }
        }

        if (collide)
        {
            xComponent = -xComponent;
            yComponent = -yComponent;
        }
    }

    private void CheckForAnts()
    {
        var ants = AntManager.Ants;
        foreach(Ant ant in ants)
        {
            if(ant.Nest.Player != this.nest.Player)
            {
                Vector2 antPosition = ant.AntGameObject.transform.position;
                float antDistance = Vector2.Distance(antPosition, this.antGameObject.transform.position);
                if (antDistance < sensorRange)
                {
                    MoveToPosition(antPosition);
                    antToFollow = ant;
                    state = "charge";
                }
            }
        }
    }

    private void Attack(Ant ant)
    {
        float randomDamage = Random.Range(0, maxDamage);
        ant.InflictDamage(randomDamage);
    }

    private void Charge()
    {
        Vector2 antPosition = antToFollow.AntGameObject.transform.position;
        float antDistance = Vector2.Distance(antPosition, this.antGameObject.transform.position);
        if (antDistance < attackRange)
        {
            xComponent = 0;
            yComponent = 0;
            Attack(antToFollow);
            state = "attacked";
        }
        else
        {
            MoveToPosition(antPosition);
        }
    }

    private void RecoverAfterAttack()
    {
        if (attackTimer <= 0)
        {
            state = "random";
            attackTimer = attackDelay;
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
    }

    public override void Move()
    {
        if (state == "random")
        {
            MoveRandomly();
            CheckForAnts();
        }
        else if (state == "charge")
        {
            Charge();
        }
        else if (state == "attacked")
        {
            RecoverAfterAttack();
        }

        CheckCollisions();
        CheckCollisionWithTerritory();

        Vector3 movementUnitVector = new Vector2(xComponent, yComponent);
        antGameObject.transform.position += movementUnitVector * Time.deltaTime * speed;
        position = antGameObject.transform.position;
    }
}
