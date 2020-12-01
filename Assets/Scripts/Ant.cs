using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Ant
{
    protected Nest nest;
    protected Vector2 position;
    protected float changeDirectionTimer;
    protected float changeDirectionFrequency;
    protected float speed;
    protected float health;
    protected int sensorRange;
    protected string state;
    protected float xComponent;
    protected float yComponent;
    protected GameObject antGameObject;
    public Nest Nest
    {
        get { return nest; }
    }
    public GameObject AntGameObject
    {
        get { return antGameObject; }
    }

    public Ant(Nest nest)
    {
        this.nest = nest;
        position = nest.Position;
        changeDirectionFrequency = 1f;
        changeDirectionTimer = 0;
    }

    protected void CheckCollisions()
    {
        // Check for collision with screen bounds
        if (antGameObject.transform.position.x < -Map.cameraWidth / 2)
            xComponent = Mathf.Abs(xComponent);
        if (antGameObject.transform.position.x > Map.cameraWidth / 2)
            xComponent = -Mathf.Abs(xComponent);
        if (antGameObject.transform.position.y < -Map.cameraHeight / 2)
            yComponent = Mathf.Abs(yComponent);
        if (antGameObject.transform.position.y > Map.cameraHeight / 2)
            yComponent = -Mathf.Abs(yComponent);
    }

    protected void MoveRandomly()
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

    protected void MoveToPosition(Vector2 position)
    {
        float distance = Vector2.Distance(position, antGameObject.transform.position);
        float dX = position.x - antGameObject.transform.position.x;
        float dY = position.y - antGameObject.transform.position.y;

        xComponent = dX / distance;
        yComponent = dY / distance;
    }

    public void InflictDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            antGameObject.SetActive(false);
            AntManager.RemoveAnt();
        }
    }

    protected abstract void CheckCollisionWithTerritory();
    public abstract void Move();
}
