using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    int numberOfColumns = 5;

    [SerializeField]
    int numberOfRows = 8;

    PlanetsPool planetsPool;
    List<BoardColumn> columns;

    float tileSize;

	// Use this for initialization
	void Start ()
    {
        // Initialize planet pool relative to number of cells 
        planetsPool = (PlanetsPool) ScriptableObject.CreateInstance("PlanetsPool");
        planetsPool.Init(numberOfColumns * numberOfRows);

        // Important to call before column creation
        GetTileSize();

        // Create board columns
        columns = new List<BoardColumn>(numberOfColumns);

        for(int i = 0; i < numberOfColumns; i++)
        {
            BoardColumn boardColumn = new BoardColumn(numberOfRows, i * tileSize, tileSize, planetsPool, LoadMaterials());
            columns.Add(boardColumn);
        }

        // Start game and fill board!
        FillBoard();
	}

    void FillBoard()
    {
        foreach (BoardColumn boardColumn in columns)
            boardColumn.Fill();
    }

    void GetTileSize()
    {
        // Create a provisional planet to get proper collision bound measurements
        GameObject tempPlanet = planetsPool.GetPlanet();
        tileSize = tempPlanet.GetComponent<SphereCollider>().bounds.extents.x * 2.1f;

        // We don't need this anymore
        tempPlanet.SetActive(false);
    }

    Material[] LoadMaterials()
    {
        return Resources.LoadAll<Material>("Planets/Materials/");
    }
}
