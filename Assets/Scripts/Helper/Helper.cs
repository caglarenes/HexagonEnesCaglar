using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static Vector3 Depth(int x, int y, int z = -1)
    {
        return new Vector3(x, y, z);
    }

    public static Vector3 Depth(Vector3 vector3, int z = -1)
    {
        return new Vector3(vector3.x, vector3.y, z);
    }
}

