using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;

    public float enemySpeed = 10f;

    public int animalLevelToLose = 6;

    public int moneyToAdd = 1000;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.velocity = -Vector3.right * enemySpeed;

        Destroy(gameObject, 15f);
    }

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
                Destroy(animal.gameObject);
            } 
        }
    }
}
