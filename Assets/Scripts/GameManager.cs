using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isPlacingLand = false;

    public LayerMask groundMask;
    public LayerMask emptyMask;

    public GameObject animalToSpawn;

    public bool isDraggingAnimal = false;

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
        if (BankManager.instance.CanBuyAnimal())
        {
            SpawnAnimal();
            BankManager.instance.RemoveMoney(BankManager.Prices.animalPrice);
        }
    }

    private void Update()
    {
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
        Instantiate(animalToSpawn, new Vector3(3, 0.62f, 3), default);
    }
}
