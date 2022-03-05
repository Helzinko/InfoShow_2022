using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid instance;

    [SerializeField] private GameObject emptyCube;
    [SerializeField] private GameObject[] landCubes;
    [SerializeField] private GameObject decCube;

    public Transform animalSpawnPlace;

    public enum CubeTypes
    {
        empty = 0,
        land = 1,
        decorative = 2,
    }

    [SerializeField] public static int gridXLenght = 7;
    [SerializeField] public static int gridZLenght = 7;

    public static int centerCoord;

    private static Cube[,] LevelGrid;

    private void Awake()
    {
        instance = this;

        centerCoord = (((gridXLenght + gridZLenght) / 2) - 1) / 2;

        LevelGrid = new Cube[gridXLenght, gridZLenght];

        for (int x = 0; x < gridXLenght; x++)
        {
            for (int z = 0; z < gridZLenght; z++)
            {
                if (x == centerCoord && z == centerCoord)
                {
                    CreateCube(x, z, CubeTypes.land, GetRandomLand());
                }
                else
                {
                    CreateCube(x, z, CubeTypes.empty, emptyCube);
                }
            }
        }
    }

    private void CreateCube(int x, int z, Grid.CubeTypes type, GameObject prefab)
    {
        var cubeObj = Instantiate(prefab, new Vector3(x, 0, z), default);
        var cube = cubeObj.GetComponent<Cube>();
        cube.SetValues(x, z, type);
        LevelGrid[x, z] = cube;
    }

    public List<Cube> GetNeighbors(int cellRow, int cellCol)
    {
        var minRow = cellRow == 0 ? 0 : cellRow - 1;
        var maxRow = cellRow == LevelGrid.GetUpperBound(0) ? cellRow : cellRow + 1;
        var minCol = cellCol == 0 ? 0 : cellCol - 1;
        var maxCol = cellCol == LevelGrid.GetUpperBound(1) ? cellCol : cellCol + 1;

        var results = new List<Cube>();

        for (int row = minRow; row <= maxRow; row++)
        {
            for (int col = minCol; col <= maxCol; col++)
            {
                if (row == cellRow && col == cellCol) continue;

                if(LevelGrid[row, col].cubeType == CubeTypes.land)
                {
                    if(!(LevelGrid[row, col].x != cellRow && LevelGrid[row, col].z != cellCol))
                        results.Add(LevelGrid[row, col]);
                }

            }
        }

        return results;
    }

    public void ReplaceCube(int x, int z)
    {
        Destroy(LevelGrid[x, z].gameObject);
        var cubeObj = Instantiate(GetRandomLand(), new Vector3(x, 0, z), default);
        var cube = cubeObj.GetComponent<Cube>();
        cube.SetValues(x, z, CubeTypes.land);
        LevelGrid[x, z] = cube;

        BankManager.instance.RemoveMoney(BankManager.Prices.landPrice);
    }

    public GameObject GetRandomLand()
    {
        return landCubes[Random.Range(0, landCubes.Length)];
    }
}
