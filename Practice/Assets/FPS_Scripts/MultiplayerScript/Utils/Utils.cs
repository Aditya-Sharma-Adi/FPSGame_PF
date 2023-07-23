using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector3 GetRandomSpawnPoint()
    {
        return new Vector3(Random.Range(-10, 10), 2, Random.Range(-10, 10));
    }

    public static void SetRenderLayerinChildren(Transform transform, int layerNumber)
    {
        foreach (Transform item in transform.GetComponentInChildren<Transform>(true))
        {
            item.gameObject.layer = layerNumber;
        }
    }
}
