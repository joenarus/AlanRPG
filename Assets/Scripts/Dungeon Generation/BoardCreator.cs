using System.Collections;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    public enum TileType
    {
        Wall, Floor,
    }

    public int columns = 100;
    public int rows = 100;
    public IntRange numRooms = new IntRange(15, 20);
    public IntRange roomWidth = new IntRange(3, 10);
    public IntRange roomHeight = new IntRange(3, 10);
    public IntRange corridorLength = new IntRange(6, 10);
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] outerWallTiles;

    private TileType[][] tiles;
    private Room[] rooms;
    private Corridor[] corridors;
    private GameObject boardHolder;

    private void Start()
    {
        boardHolder = new GameObject("BoardHolder");

        SetUpTilesArray();

        CreateRoomsAndCorridors();

        SetTilesValuesForRooms();
        SetTilesValuesForCorridors();

        InstantiateTiles();
        InstantiateOuterWalls();
    }

    void SetUpTilesArray()
    {
        tiles = new TileType[columns][];

        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = new TileType[rows];
        }
    }

    void CreateRoomsAndCorridors()
    {
        rooms = new Room[numRooms.Random];

        corridors = new Corridor[rooms.Length - 1];

        rooms[0] = new Room();
        corridors[0] = new Corridor();

        rooms[0].SetupRoom(roomWidth, roomHeight, columns, rows);
        corridors[0].SetupCorridor(rooms[0], corridorLength, roomWidth, roomHeight, columns, rows, true);

        for (int i = 1; i < rooms.Length; i++)
        {
            rooms[i] = new Room();
            rooms[i].SetupRoom(roomWidth, roomHeight, columns, rows, corridors[i - 1]);
            if (i < corridors.Length)
            {
                corridors[i] = new Corridor();

                corridors[i].SetupCorridor(rooms[i], corridorLength, roomWidth, roomHeight, columns, rows, false);
            }
        }
    }

    void SetTilesValuesForRooms()
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            Room currentRoom = rooms[i];
            for (int j = 0; j < currentRoom.roomWidth; j++)
            {
                int xCoord = currentRoom.xPos + j;

                for (int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int zCoord = currentRoom.zPos + k;

                    tiles[xCoord][zCoord] = TileType.Floor;
                }
            }
        }
    }

    void SetTilesValuesForCorridors()
    {
        for (int i = 0; i < corridors.Length; i++)
        {
            Corridor currentCorridor = corridors[i];
            for (int j = 0; j < currentCorridor.corridorLength; j++)
            {
                int xCoord = currentCorridor.startXPos;
                int zCoord = currentCorridor.startZPos;

                switch (currentCorridor.direction)
                {
                    case Direction.North:
                        zCoord += j;
                        break;
                    case Direction.East:
                        xCoord += j;
                        break;
                    case Direction.South:
                        zCoord -= j;
                        break;
                    case Direction.West:
                        xCoord -= j;
                        break;
                }
                tiles[xCoord][zCoord] = TileType.Floor;
            }
        }
    }

    void InstantiateTiles()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            for (int j = 0; j < tiles[i].Length; j++)
            {
                InstantiateFromArray(floorTiles, i, j);

                if (tiles[i][j] == TileType.Wall)
                {
                    InstantiateFromArray(wallTiles, i, j);
                }
            }
        }
    }
    void InstantiateOuterWalls()
    {
        float leftEdgeX = -1f;
        float rightEdgeX = columns + 0f;
        float bottomEdgeZ = -1f;
        float topEdgeZ = rows + 0f;

        InstantiateVerticalOuterWall(leftEdgeX, bottomEdgeZ, topEdgeZ);
        InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeZ, topEdgeZ);

        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeZ);
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeZ);
    }

    void InstantiateVerticalOuterWall(float xCoord, float startingZ, float endingZ)
    {
        float currentZ = startingZ;

        while (currentZ <= endingZ)
        {
            InstantiateFromArray(outerWallTiles, xCoord, currentZ);
            currentZ++;
        }
    }

    void InstantiateHorizontalOuterWall(float startingX, float endingX, float zCoord)
    {
        float currentX = startingX;

        while (currentX <= endingX)
        {
            InstantiateFromArray(outerWallTiles, currentX, zCoord);
            currentX++;
        }
    }

    void InstantiateFromArray(GameObject[] prefabs, float xCoord, float zCoord)
    {
        int randomIndex = Random.Range(0, prefabs.Length);

        Vector3 position = new Vector3(xCoord, 0f, zCoord);

        GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

        tileInstance.transform.parent = boardHolder.transform;
    }
}