using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cube : MonoBehaviour
{
    public int x;
    public int z;
    public Grid.CubeTypes cubeType;

    public void SetValues(int x, int z, Grid.CubeTypes cubeType)
    {
        this.x = x;
        this.z = z;
        this.cubeType = cubeType;
    }
}
