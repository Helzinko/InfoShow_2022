using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCube : Cube
{
    [SerializeField] private MeshRenderer rend;
    private Cube cube;

    [SerializeField] private Color baseColor;
    [SerializeField] private Color mouseOverColor;

    // Start is called before the first frame update
    void Start()
    {
        cube = GetComponent<Cube>();
        TogglePlacingIndication();
        rend.material.color = baseColor;
    }

    private void OnMouseOver()
    {
        rend.material.color = mouseOverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = baseColor;
    }

    private void OnMouseDown()
    {
        if (GameManager.instance.isPlacingLand)
        {
            Grid.instance.ReplaceCube(x, z, Grid.instance.GetRandomLand());
            SoundManager.instance.PlayEffect(GameType.SoundTypes.place_land);
            GameManager.instance.StartPlacingLand();
        }
    }

    public void TogglePlacingIndication()
    {
        rend.material.color = baseColor;
        rend.enabled = GameManager.instance.isPlacingLand;
    }
}
