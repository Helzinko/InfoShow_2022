using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isPlacingLand = false;

    public LayerMask groundMask;
    public LayerMask emptyMask;

    public GameObject boxToSpawn;

    public bool isDraggingAnimal = false;

    public bool canSpawnBox = true;

    [SerializeField] private GameObject boxSpawnParticle;

    [SerializeField] private GameObject gameOverText;

    public int currentBoxLevel = 0;

    public bool playerLost = false;

    public AudioSource hurtSource;
    public AudioSource mergeSource;
    public AudioSource boxBuySource;
    public AudioSource boxOpenSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SpawnAnimal();
    }

    public void StartPlacingLand()
    {
        if (isPlacingLand)
        {
            isPlacingLand = !isPlacingLand;
        }
        else
        {
            if (BankManager.instance.CanBuyLand())
            {
                isPlacingLand = !isPlacingLand;
            }
        }
    }

    public void BuyAnimal()
    {
        if (BankManager.instance.CanBuyAnimal() && canSpawnBox)
        {
            SpawnAnimal();
            BankManager.instance.RemoveMoney(BankManager.Prices.animalPrice);
        }
    }

    public void BuyUpgrade()
    {
        if (BankManager.instance.CanUpgradeBox())
        {
            currentBoxLevel++;
            BankManager.instance.RemoveMoney(BankManager.Prices.boxUpradePrice);
        }
    }

    private void Update()
    {
        if(playerLost && Input.GetKeyDown(KeyCode.Return))
        {
            PauseMenu.instance.Restart();
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.E))
        {
            BankManager.instance.AddMoney(100);
        }
#endif
    }

    private void SpawnAnimal()
    {
        // debug coords
        Instantiate(boxToSpawn, new Vector3(3, 0.5f, 3), default);
        Destroy(Instantiate(boxSpawnParticle, new Vector3(3, 0.5f, 3), default), 1f);
        boxBuySource.Play();
        canSpawnBox = false;
    }

    [SerializeField] private GameObject StartText;
    private bool firstBox = true;
    public void CheckIfFirstBox()
    {
        if (firstBox)
        {
            StartText.SetActive(false);
            firstBox = false;
        }
    }

    public void CheckBirdCount(int minus = 0)
    {
        var birds = FindObjectsOfType<Animal>();

        if(birds.Length - minus == 0)
        {
            PauseMenu.instance.gameplayUI.SetActive(false);
            gameOverText.SetActive(true);
            playerLost = true;
        }
    }
}
