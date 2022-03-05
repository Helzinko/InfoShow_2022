using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    private Vector3[] randomDir = { Vector3.right, Vector3.left, Vector3.forward, Vector3.back };

    private float moveDistance;
    private float checkRadius;

    public float movSpeed = 0.3f;
    public float timeBetweenMov = 2.0f;

    public int moneyValue = 1;

    public float timeBetweenClicks = 1f;
    private float timeSinceLastClick = 0;

    private float originalSize;

    private Vector3 screenPoint;
    private Vector3 offset;

    public int AnimalLevel = 1;

    private AnimalDrag animalDrag;

    private void Start()
    {
        moveDistance = transform.localScale.z;

        originalSize = transform.localScale.x;

        checkRadius = moveDistance / 5;

        timeSinceLastClick = timeBetweenClicks;

        animalDrag = GetComponent<AnimalDrag>();

        StartCoroutine(Movement());
        StartCoroutine(MoneyPerSec());
    }

    private void Update()
    {
        timeSinceLastClick += Time.deltaTime;
    }

    private Vector3 checkDir;

    IEnumerator Movement()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenMov);
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
            transform.DOMove(dir, movSpeed);
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
        if (!GameManager.instance.isPlacingLand && !GameManager.instance.isDraggingAnimal)
        {
            if(timeSinceLastClick > timeBetweenClicks)
            {
                BankManager.instance.AddMoney(moneyValue);
                StartCoroutine(ClickedAnimation());
                timeSinceLastClick = 0;
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
            BankManager.instance.AddMoney(moneyValue);
        }
    }
}
