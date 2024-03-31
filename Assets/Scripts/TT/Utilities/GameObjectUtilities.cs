using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectUtilities
{
    public static GameObject FindObjectByTag(Transform parent, string tag)
    {
        if (parent.CompareTag(tag))
        {
            return parent.gameObject;
        }

        foreach (Transform child in parent)
        {
            GameObject result = FindObjectByTag(child, tag);

            if (result != null)
            {
                return result;
            }
        }

        return null;
    }

    public static GameObject FindObjectByName(Transform parent, string name)
    {
        if (parent.CompareTag(name))
        {
            return parent.gameObject;
        }

        foreach (Transform child in parent)
        {
            GameObject result = FindObjectByName(child, name);

            if (result != null)
            {
                return result;
            }
        }

        return null;
    }

    public static List<Transform> GetChildren(Transform parent)
    {
        List<Transform> result = new List<Transform>();
        foreach (Transform child in parent)
        {
            if (child.Equals(parent)) continue;
            result.Add(child);
        }

        return result;
    }
}
