using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;

    public float enemySpeed = 10f;

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
            Destroy(animal.gameObject);
        }
    }
}
