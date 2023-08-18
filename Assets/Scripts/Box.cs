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

        SoundManager.instance.PlayEffect(GameType.SoundTypes.box_open);

        Destroy(Instantiate(openParticle, transform.position, default), 1f);

        var animalToSpawn = AnimalSpawner.instance.GetAnimal();
        var animalPrefab = Instantiate(animalToSpawn, new Vector3(3, 0.5f, 3), default);
        animalPrefab.GetComponent<Animal>().SetupAnimal(1, 1, false);

        GameManager.instance.canSpawnBox = true;
        Destroy(gameObject);
    }
}
