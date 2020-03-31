using UnityEngine;

public enum PathDirection
{
    North, East, South, West
}

public class Corridor
{
    public int startXPos;
    public int startZPos;
    public int corridorLength;
    public PathDirection direction;

    public int EndPositionX
    {
        get
        {
            if (direction == PathDirection.North || direction == PathDirection.South)
                return startXPos;
            if (direction == PathDirection.East)
                return startXPos + corridorLength - 1;
            return startXPos - corridorLength + 1;
        }
    }

    public int EndPositionZ
    {
        get
        {
            if (direction == PathDirection.East || direction == PathDirection.West)
                return startZPos;
            if (direction == PathDirection.North)
                return startZPos + corridorLength - 1;
            return startZPos - corridorLength + 1;
        }
    }

    public void SetupCorridor(Room room, IntRange length, IntRange roomWidth, IntRange roomHeight, int columns, int rows, bool firstCorridor)
    {
        direction = (PathDirection)Random.Range(0, 4);

        PathDirection oppositeDirection = (PathDirection)(((int)room.enteringCorridor + 2) % 4);

        if (!firstCorridor && direction == oppositeDirection)
        {
            int directionInt = (int)direction;
            directionInt++;
            directionInt = directionInt % 4;
            direction = (PathDirection)directionInt;
        }
        corridorLength = length.Random;
        int maxLength = length.m_Max;

        switch (direction)
        {
            case PathDirection.North:
                startXPos = Random.Range(room.xPos, room.xPos + room.roomWidth - 1);
                startZPos = room.zPos + room.roomHeight;
                maxLength = rows - startZPos - roomHeight.m_Min;
                break;
            case PathDirection.East:
                startXPos = room.xPos + room.roomWidth;
                startZPos = Random.Range(room.zPos, room.zPos + room.roomHeight - 1);
                maxLength = columns - startXPos - roomWidth.m_Min;
                break;
            case PathDirection.South:
                startXPos = Random.Range(room.xPos, room.xPos + room.roomWidth);
                startZPos = room.zPos;
                maxLength = startZPos - roomHeight.m_Min;
                break;
            case PathDirection.West:
                startXPos = room.xPos;
                startZPos = Random.Range(room.zPos, room.zPos + room.roomHeight);
                maxLength = startXPos - roomWidth.m_Min;
                break;
        }
        corridorLength = Mathf.Clamp(corridorLength, 1, maxLength);
    }
}