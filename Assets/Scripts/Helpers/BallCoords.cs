using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCoords
{
    public float[] x;
    public float[] y;
    public float[] z;

    public Vector3[] GetBallPath()
    {
        Vector3[] ballPath = new Vector3[x.Length];

        for (int i = 0; i < ballPath.Length; i++)
        {
            ballPath[i].Set(x[i], y[i], z[i]);
        }

        return ballPath;
    }
}