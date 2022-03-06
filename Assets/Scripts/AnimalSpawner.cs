using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public static AnimalSpawner instance;

    [SerializeField] private List<GameObject> animals;

    [SerializeField] public GameObject mergeParticle;

    [SerializeField] public GameObject deathParticlearticle;

    public GameObject popupText;

    public GameObject cameraHolder;

    private void Awake()
    {
        instance = this;
    }

    public GameObject GetAnimal(int level)
    {
        return animals[level];
    }
}
