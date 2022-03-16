using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    private Vector3[] randomDir = { Vector3.right, Vector3.left, Vector3.forward, Vector3.back };

    private float moveDistance;
    private float checkRadius;

    private float timeSinceLastClick = 0;

    private float originalSize;

    private Vector3 screenPoint;
    private Vector3 offset;

    private AnimalDrag animalDrag;

    private Vector3 checkDir;

    private int level = 1;
    private int coinsPerSecond = 1;
    private bool canKillEnemy = false;

    private void Start()
    {
        moveDistance = transform.localScale.z;

        originalSize = transform.localScale.x;

        checkRadius = moveDistance / 5;

        timeSinceLastClick = AnimalManager.instance.timeBetweenClicks;

        animalDrag = GetComponent<AnimalDrag>();

        StartCoroutine(Movement());
        StartCoroutine(MoneyPerSec());
    }

    private void Update()
    {
        timeSinceLastClick += Time.deltaTime;
    }

    IEnumerator Movement()
    {
        while (true)
        {
            yield return new WaitForSeconds(AnimalManager.instance.timeBetweenMov);
            if (!animalDrag.dragging)
            {
                if (!CheckDistance())
                {
                    bool canContinue = false;

                    int stepCount = 1000;
                    while (!canContinue && stepCount > 0)
                    {
                        stepCount--;
                        if (CheckDistance())
                        {
                            canContinue = true;
                        }
                    }
                }
            }
        }
    }

    private bool CheckDistance()
    {
        var dir = transform.position + (randomDir[Random.Range(0, randomDir.Length)] * moveDistance);
        checkDir = new Vector3(dir.x, dir.y - transform.localScale.y, dir.z);
        if (Physics.CheckSphere(checkDir, checkRadius, GameManager.instance.groundMask) && !Physics.CheckSphere(checkDir, checkRadius, GameManager.instance.emptyMask))
        {
            transform.LookAt(dir);
            transform.DOMove(dir, AnimalManager.instance.movSpeed);
            SoundManager.instance.PlayEffect(GameType.SoundTypes.bird_move);
            return true;
        }

        return false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(checkDir, checkRadius);
    }

    private void OnMouseDown()
    {
        if (PauseMenu.instance.isPaused) return;

        if (!GameManager.instance.isPlacingLand && !GameManager.instance.isDraggingAnimal)
        {
            if(timeSinceLastClick > AnimalManager.instance.timeBetweenClicks)
            {
                BankManager.instance.AddMoney(coinsPerSecond);
                StartCoroutine(ClickedAnimation());
                ShowPopupText();
                timeSinceLastClick = 0;
                SoundManager.instance.PlayEffect(GameType.SoundTypes.bird_punch);
            }
        }
    }

    private float animTime = 0.1f;
    IEnumerator ClickedAnimation()
    {
        transform.DOScale(originalSize * 2, animTime);
        yield return new WaitForSeconds(animTime);
        transform.DOScale(originalSize, animTime);
    }

    IEnumerator MoneyPerSec()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            BankManager.instance.AddMoney(coinsPerSecond);

            ShowPopupText();
        }
    }

    private void ShowPopupText()
    {
        GameObject floatingText = Instantiate(GameManager.instance.popupText, transform.position, Quaternion.identity);
        floatingText.GetComponent<PopupText>().displayText = "+" + coinsPerSecond;
    }

    public int GetLevel()
    {
        return level;
    }

    public bool CanKillEnemy()
    {
        return canKillEnemy;
    }

    public void SetupAnimal(int level, int coinsPerSecond, bool canKillEnemy)
    {
        this.level = level;
        this.coinsPerSecond = coinsPerSecond;
        this.canKillEnemy = canKillEnemy;
    }
}
