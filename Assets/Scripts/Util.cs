using UnityEngine;

public class Util
{
    public static T FindObjectOfTypeOrLogError<T>() where T : Object
    {
        var found = Object.FindFirstObjectByType<T>();
        if (found is null)
        {
            Debug.LogError($"{typeof(T).Name} not found in the scene");
        }
        return found;
    }
}