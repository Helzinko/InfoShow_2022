using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;

    public float enemySpeed = 10f;

    public int animalLevelToLose = 6;

    public int moneyToAdd = 1000;

    private bool canMove = false;
    private Vector3 moveDir;

    private void OnTriggerEnter(Collider other)
    {
        var animal = other.GetComponent<Animal>();

        if (animal)
        {
            if(animal.AnimalLevel >= 6)
            {
                Destroy(Instantiate(AnimalSpawner.instance.deathParticlearticle, transform.position, default), 1f);
                BankManager.instance.AddMoney(moneyToAdd);
                Destroy(gameObject);
            }
            else
            {
                GameManager.instance.CheckBirdCount(1);
                Destroy(Instantiate(AnimalSpawner.instance.deathParticlearticle, animal.gameObject.transform.position, default), 1f);
                SoundManager.instance.PlayEffect(SoundTypes.bird_hurt);
                Destroy(animal.gameObject);
            } 
        }
    }

    public void Move(Vector3 direction)
    {
        rb = GetComponent<Rigidbody>();
        moveDir = direction;
        canMove = true;
        Destroy(gameObject, 15f);
        SoundManager.instance.PlayEffect(SoundTypes.eagle_spawn);
    }

    private void FixedUpdate()
    {
        if (!canMove) return;

        rb.velocity = moveDir * enemySpeed * Time.deltaTime;
    }
}
