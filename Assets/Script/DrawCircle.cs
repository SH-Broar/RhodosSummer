using UnityEngine;

public static class GameObjectEx
{
    public static void DrawCircle(this GameObject container, float radius, ref LineRenderer LR)
    {
        var points = new Vector3[361];

        for (int i = 0; i < 361; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / 360f);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, Mathf.Cos(rad) * radius, 0);
        }

        LR.SetPositions(points);
    }
}