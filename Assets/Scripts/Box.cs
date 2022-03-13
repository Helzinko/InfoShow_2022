using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private GameObject openParticle;

    private void OnMouseDown()
    {
        if (PauseMenu.instance.isPaused) return;

        GameManager.instance.CheckIfFirstBox();

        SoundManager.instance.PlayEffect(SoundTypes.box_open);

        Destroy(Instantiate(openParticle, transform.position, default), 1f);
        Instantiate(AnimalSpawner.instance.GetAnimal(GameManager.instance.currentBoxLevel), new Vector3(3, 0.62f, 3), default);
        GameManager.instance.canSpawnBox = true;
        Destroy(gameObject);
    }
}
