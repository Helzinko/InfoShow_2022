using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;

    [HideInInspector] public bool isPlacingObject = false;

    private ObjectSO objectForPlacemenet;

    private void Awake()
    {
        instance = this;
    }

    public void BuyObject(ObjectSO objectToPlace)
    {
        if (isPlacingObject)
        {
            StopPlacingObject();
        }
        else
        {
            if (BankManager.instance.CanBuyObject(objectToPlace.price) && !GameManager.instance.isPlacingLand)
            {
                Grid.instance.TogglePlacingIndicators(true);
                isPlacingObject = true;
                objectForPlacemenet = objectToPlace;
            }
            else
            {
                SoundManager.instance.PlayEffect(GameType.SoundTypes.ui_error);
            }
        }

    }

    public void PlaceObject(GameObject landCube)
    {
        StopPlacingObject();

        if (BankManager.instance.CanBuyObject(objectForPlacemenet.price))
        {
            BankManager.instance.RemoveMoney(objectForPlacemenet.price);
            landCube.GetComponent<LandCube>().isFull = true;

            var spawningPointY = landCube.transform.position.y + (landCube.transform.lossyScale.y / 2) + (objectForPlacemenet.prefab.transform.lossyScale.y / 2);

            var placedObject = Instantiate(objectForPlacemenet.prefab, new Vector3(landCube.transform.position.x, spawningPointY, landCube.transform.position.z), default);
        }
        else
        {
            SoundManager.instance.PlayEffect(GameType.SoundTypes.ui_error);
        }
    }

    public void StopPlacingObject()
    {
        Grid.instance.TogglePlacingIndicators(false);
        isPlacingObject = false;
    }
}
