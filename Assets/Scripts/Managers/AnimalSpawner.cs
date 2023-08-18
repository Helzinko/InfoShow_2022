using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalSpawner : MonoBehaviour
{
    [SerializeField] private Animal animal;

    public static AnimalSpawner instance;

    //[SerializeField] private List<AnimalSO> animals;

    [SerializeField] public GameObject mergeParticle;

    [SerializeField] public GameObject deathParticlearticle;

    public GameObject cameraHolder;

    public int currentHighestBird = 0;

    [SerializeReference] private GameObject BirdCard;

    private void Awake()
    {
        instance = this;
    }

    public Animal GetAnimal()
    {
        return animal;
    }

    public void CheckRecord(int unlockedLevel, AnimalSO animalSO)
    {
        if(currentHighestBird < unlockedLevel)
        {
            currentHighestBird = unlockedLevel;

            var card = BirdCard.GetComponent<Card>();
            card.SetupCard(animalSO.birdImage, animalSO.level.ToString(), animalSO.cointPerSecond.ToString(), animalSO.canKillEnemy);

            BirdCard.SetActive(true);

            PauseMenu.instance.Pause();
        }
    }
}
