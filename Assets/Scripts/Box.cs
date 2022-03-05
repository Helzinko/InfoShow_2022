using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private void OnMouseDown()
    {
        Instantiate(AnimalSpawner.instance.GetAnimal(0), new Vector3(3, 0.62f, 3), default);
        Destroy(gameObject);
        GameManager.instance.canSpawnBox = true;
    }
}
