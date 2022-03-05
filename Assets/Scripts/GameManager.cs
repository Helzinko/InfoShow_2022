using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isPlacingLand = false;

    [SerializeField] private GameObject placingIndicator;

    public LayerMask groundMask;
    public LayerMask emptyMask;

    private void Awake()
    {
        instance = this;
    }

    public void StartPlacingLand()
    {
        isPlacingLand = !isPlacingLand;
        placingIndicator.SetActive(isPlacingLand);
    }
}
