using UnityEngine;

public static class Transforms
{

    public static void DestroyChildren(this Transform t, bool destroyImmiediately = false)
    {
        foreach (Transform child in t)
        {
            if (destroyImmiediately)
                MonoBehaviour.DestroyImmediate(child.gameObject);
            else
                MonoBehaviour.Destroy(child.gameObject);
        }
    }

}