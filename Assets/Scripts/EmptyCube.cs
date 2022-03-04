using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCube : Cube
{
    private MeshRenderer rend;

    private Cube cube;

    // Start is called before the first frame update
    void Start()
    {
        cube = GetComponent<Cube>();
        rend = GetComponent<MeshRenderer>();
        rend.enabled = false;
    }

    private void OnMouseOver()
    {
        if(GameManager.instance.isPlacingLand)
            rend.enabled = true;
    }

    private void OnMouseExit()
    {
        rend.enabled = false;
    }

    private void OnMouseDown()
    {
        if (GameManager.instance.isPlacingLand)
        {
            if (Grid.instance.GetNeighbors(x, z).Count > 0)
            {
                Grid.instance.ReplaceCube(x, z);
                GameManager.instance.StartPlacingLand();
            }
        }
    }
}
