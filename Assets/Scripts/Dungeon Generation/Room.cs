using UnityEngine;

public class Room
{
    public int xPos;
    public int zPos;
    public int roomWidth;
    public int roomHeight;
    public PathDirection enteringCorridor;

    public void SetupRoom(IntRange widthRange, IntRange heightRange, int columns, int rows)
    {
        roomWidth = widthRange.Random;
        roomHeight = heightRange.Random;

        xPos = Mathf.RoundToInt(columns / 2f - roomWidth / 2f);
        zPos = Mathf.RoundToInt(rows / 2f - roomHeight / 2f);
    }

    public void SetupRoom(IntRange widthRange, IntRange heightRange, int columns, int rows, Corridor corridor)
    {
        enteringCorridor = corridor.direction;

        roomWidth = widthRange.Random;
        roomHeight = heightRange.Random;

        switch (corridor.direction)
        {
            case PathDirection.North:
                roomHeight = Mathf.Clamp(roomHeight, 1, rows - corridor.EndPositionZ);
                zPos = corridor.EndPositionZ;

                xPos = Random.Range(corridor.EndPositionX - roomWidth + 1, corridor.EndPositionX);
                xPos = Mathf.Clamp(xPos, 0, columns - roomWidth);
                break;
            case PathDirection.East:
                roomWidth = Mathf.Clamp(roomWidth, 1, columns - corridor.EndPositionX);
                xPos = corridor.EndPositionX;

                zPos = Random.Range(corridor.EndPositionZ - roomHeight + 1, corridor.EndPositionZ);
                zPos = Mathf.Clamp(zPos, 0, rows - roomHeight);
                break;
            case PathDirection.South:
                roomHeight = Mathf.Clamp(roomHeight, 1, corridor.EndPositionZ);
                zPos = corridor.EndPositionZ - roomHeight + 1;

                xPos = Random.Range(corridor.EndPositionX - roomWidth + 1, corridor.EndPositionX);
                xPos = Mathf.Clamp(xPos, 0, columns - roomWidth);
                break;
            case PathDirection.West:
                roomWidth = Mathf.Clamp(roomWidth, 1, corridor.EndPositionX);
                xPos = corridor.EndPositionX - roomWidth + 1;
                
                zPos = Random.Range(corridor.EndPositionZ - roomHeight + 1, corridor.EndPositionZ);
                zPos = Mathf.Clamp(zPos, 0, rows - roomHeight);
                break;
        }
    }
}