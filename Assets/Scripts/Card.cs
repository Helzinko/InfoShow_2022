using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public void OkayButton()
    {
        PauseMenu.instance.Resume();
        gameObject.SetActive(false);
    }
}
