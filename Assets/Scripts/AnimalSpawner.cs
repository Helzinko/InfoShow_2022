using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalSpawner : MonoBehaviour
{
    public static AnimalSpawner instance;

    [SerializeField] private List<GameObject> animals;

    [SerializeField] public GameObject mergeParticle;

    [SerializeField] public GameObject deathParticlearticle;

    public GameObject popupText;

    public GameObject cameraHolder;

    public Sprite[] birdImages;
    public int currentHighestBird = 0;

    [SerializeReference] private GameObject BirdCard;
    [SerializeReference] private Image cardImage;

    private void Awake()
    {
        instance = this;
    }

    public GameObject GetAnimal(int level)
    {
        return animals[level];
    }

    public void CheckRecord(int unlockedLevel)
    {
        if(currentHighestBird < unlockedLevel)
        {
            currentHighestBird = unlockedLevel;

            cardImage.sprite = birdImages[currentHighestBird];
            BirdCard.SetActive(true);

            PauseMenu.instance.Pause();
        }
    }
}
