using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
     public static AnimalSpawner instance;

    [SerializeField] private List<GameObject> animals;

    private void Awake()
    {
        instance = this;
    }

    public GameObject GetAnimal(int level)
    {
        return animals[level];
    }
}
