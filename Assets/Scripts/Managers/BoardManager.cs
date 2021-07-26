using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : Singleton<BoardManager>
{
    public int boardSizeX;
    public int boardSizeY;

    public GameObject hexagonTilePrefab;
    public GameObject hexagonPrefab;
    public GameObject hexagonBombPrefab;

    public GameObject hexagonGroupPrefab;

    public GameObject hexagonsParent;
    public GameObject hexagonGroupsParent;
    public List<HexagonGroup> hexagonGroups = new List<HexagonGroup>();

    public HexagonTile[,] hexagonTilesArray = new HexagonTile[8, 9];
    public List<HexagonTile> hexagonTilesList = new List<HexagonTile>();

    public List<BombHexagon> bombHexagons = new List<BombHexagon>();

    public void SetupBoard()
    {
        CreateBoard();
        CreateHexagonGroup();
        CheckBoardForStart();
    }

    private void CheckBoardForStart()
    {
        while (!ClearMatches())
        {
            FillEmptyTiles();
        }
    }

    void CreateBoard()
    {
        hexagonTilesArray = new HexagonTile[boardSizeX, boardSizeY];

        for (int i = 0; i < boardSizeX; i++)
        {
            for (int k = 0; k < boardSizeY; k++)
            {
                Vector3 instantiatePosition = HexagonTile.PositionCalculator(i, k);
                GameObject createdHexagonTileObject = Instantiate(hexagonTilePrefab, instantiatePosition, Quaternion.identity);
                createdHexagonTileObject.transform.SetParent(gameObject.transform, true);

                var hexagonTileComponent = createdHexagonTileObject.GetComponent<HexagonTile>();
                hexagonTilesArray[i, k] = hexagonTileComponent;
                hexagonTilesList.Add(hexagonTileComponent);
                hexagonTileComponent.hexagonX = i;
                hexagonTileComponent.hexagonY = k;
                hexagonTileComponent.FillTile();
            }
        }
    }

    void CreateHexagonGroup()
    {

        //Create Right Groups
        for (int i = 0; i < boardSizeX - 1; i++)
        {
            for (int k = 0; k < boardSizeY - 1; k++)
            {
                Vector3 instantiatePosition = HexagonGroup.PositionRightCalculator(i, k);
                GameObject createdHexagonGroupRightObject = Instantiate(hexagonGroupPrefab, instantiatePosition, Quaternion.identity);
                HexagonGroup createdHexagonGroupRightComponent = createdHexagonGroupRightObject.GetComponent<HexagonGroup>();
                hexagonGroups.Add(createdHexagonGroupRightComponent);
                createdHexagonGroupRightComponent.SetGroupMembers(i, k, true);
                createdHexagonGroupRightComponent.CreateCollider();
                createdHexagonGroupRightObject.transform.SetParent(hexagonGroupsParent.transform, true);
            }
        }

        //Create Left Groups

        for (int i = 0; i < boardSizeX - 1; i++)
        {
            for (int k = 0; k < boardSizeY - 1; k++)
            {
                if (((0 < i) && (i < boardSizeX)))
                {
                    Vector3 instantiatePosition = HexagonGroup.PositionLeftCalculator(i, k);
                    GameObject createdHexagonGroupLeftObject = Instantiate(hexagonGroupPrefab, instantiatePosition, Quaternion.identity);
                    createdHexagonGroupLeftObject.transform.SetParent(hexagonGroupsParent.transform, true);

                    HexagonGroup createdHexagonGroupLeftComponent = createdHexagonGroupLeftObject.GetComponent<HexagonGroup>();
                    createdHexagonGroupLeftComponent.isLeft = true;
                    hexagonGroups.Add(createdHexagonGroupLeftComponent);

                    createdHexagonGroupLeftComponent.SetGroupMembers(i, k, false);
                    createdHexagonGroupLeftComponent.CreateCollider();
                }
            }
        }

        // Create Last Left Group
        for (int k = 0; k < boardSizeY - 1; k++)
        {
            int i = boardSizeX - 1;
            if (((0 < i) && (i <= boardSizeX)))
            {
                Vector3 instantiatePosition2 = HexagonGroup.PositionLeftCalculator(i, k);
                GameObject createdHexagonGroupLeftObject = Instantiate(hexagonGroupPrefab, instantiatePosition2, Quaternion.identity);
                createdHexagonGroupLeftObject.transform.SetParent(hexagonGroupsParent.transform, true);

                HexagonGroup createdHexagonGroupLeftComponent = createdHexagonGroupLeftObject.GetComponent<HexagonGroup>();
                createdHexagonGroupLeftComponent.isLeft = true;
                hexagonGroups.Add(createdHexagonGroupLeftComponent);

                createdHexagonGroupLeftComponent.SetGroupMembers(i, k, false);
                createdHexagonGroupLeftComponent.CreateCollider();
            }

        }
    }

    public bool CheckAllTiles()
    {
        bool isAllClear = true;

        foreach (var hexGroup in hexagonGroups)
        {
            if (hexGroup.CheckGroup())
            {
                isAllClear = false;
            }
        }

        return isAllClear;
    }

    public bool CheckBombs()
    {
        foreach (var item in bombHexagons)
        {
            if (item.moveLeft <= 0)
            {
                return true;
            }
        }

        return false;
    }
    
    public bool ClearMatches()
    {
        bool isAllClear = true;

        foreach (var hexGroup in hexagonGroups)
        {
            if (hexGroup.CheckGroup(true))
            {
                isAllClear = false;
            }
        }

        return isAllClear;
    }

    public void FillEmptyTiles()
    {
        foreach (var item in hexagonTilesList)
        {
            if (item.onHexagon is null)
            {
                item.FillTile();
            }
        }
    }

    public bool IsThereAvailableMove()
    {
        bool isThereAvailableMove = false;
        foreach (var item in hexagonGroups)
        {
            item.SwapItems();
            if (!CheckAllTiles())
            {
                isThereAvailableMove = true;
            }
            item.SwapItems();
            if (!CheckAllTiles())
            {
                isThereAvailableMove = true;
            }
            item.SwapItems();

            if (isThereAvailableMove)
            {
                return true;
            }
        }
        return false;
    }

    public void AddBombToList(BombHexagon bombHexagon)
    {
        bombHexagons.Add(bombHexagon);
    }

    public void RemoveBombFromList(BombHexagon bombHexagon)
    {
        bombHexagons.Remove(bombHexagon);
    }

    public void UpdateBombHexagons()
    {
        foreach (var item in bombHexagons)
        {
            item.UpdateMoves();
        }
    }

}
