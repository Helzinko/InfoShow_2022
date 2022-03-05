using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class BankManager : MonoBehaviour
{
    public static BankManager instance;

    private int currentMoney = 0;

    public TextMeshProUGUI moneyText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateMoneyText();
    }

    public void AddMoney(int ammount)
    {
        currentMoney += ammount;
        UpdateMoneyText();
    }

    private void UpdateMoneyText()
    {
        moneyText.text = "Money: " + currentMoney;
    }
}
