using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBoard : MonoBehaviour
{
    public BattleManager manager;
    public Plane plane;

    public int width;
    public int length;
    public float halfWidth = 5;
    public float halfLength = 5;
    public List<float> xCenter;
    public List<float> yCenter;

    public void InitBoard()
    {
        xCenter = new();
        yCenter = new();
        float xDiff = halfWidth * 2 / width;
        float xTemp = halfWidth * 2 / -2 + xDiff / 2;
        float yDiff = halfWidth * 2 / length;
        float yTemp = halfWidth * 2 / -2 + yDiff / 2;
        for (int i = 0; i < width; i++)
        {
            xCenter.Add(xTemp + (xDiff * i));
            manager.gamePieces.Add(new List<GamePiece>());
        }
        for (int i = 0; i < length; i++)
        {
            yCenter.Add(yTemp + (yDiff * i));
            foreach (List<GamePiece> list in manager.gamePieces)
            {
                list.Add(null);
            }
        }

        plane = new(Vector3.up, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public (int x, int y) GetClosestGrid(Vector3 vector)
    {
        if (Mathf.Abs(vector.x) > halfWidth || Mathf.Abs(vector.z) > halfLength)
        {
            Debug.Log("out of bounds");
            return (-1, -1);
        }

        int x = 0;

        int low = 0;
        int high = width - 1;
        int mid = 0;

        while (low <= high)
        {
            mid = (low + high) / 2;

            if (xCenter[mid] < vector.x)
            {
                low = mid + 1;
            }
            else
            {
                high = mid - 1;
            }
        }

        if (low > width - 1)
        {
            x = high;
        }
        else if (high < 0)
        {
            x = low;
        }
        else
        {
            if (Mathf.Abs(xCenter[low] - vector.x) > Mathf.Abs(xCenter[high] - vector.x))
            {
                x = high;
            }
            else
            {
                x = low;
            }
        }

        int y = 0;

        low = 0;
        high = length - 1;
        mid = 0;

        while (low <= high)
        {
            mid = (low + high) / 2;

            if (yCenter[mid] < vector.z)
            {
                low = mid + 1;
            }
            else
            {
                high = mid - 1;
            }
        }

        if (low > length - 1)
        {
            y = high;
        }
        else if (high < 0)
        {
            y = low;
        }
        else
        {
            if (Mathf.Abs(yCenter[low] - vector.z) > Mathf.Abs(yCenter[high] - vector.z))
            {
                y = high;
            }
            else
            {
                y = low;
            }
        }

        Debug.Log($"x {x} y {y}");
        return (x, y);
    }
}
