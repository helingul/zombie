using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary> Class <c>Utility</c> contains the utility functions for the game. </summary>
public class Utils : MonoBehaviour
{
    /// <summary> Method <c>FindWithTag</c> finds the first child object of the transform that has the parameter tag. </summary>
    public static Transform FindWithTag(Transform transform, string tag)
    {
        var childCount = transform.childCount;
        Transform output = null;
        for (var i = 0; i < childCount; i++)
        {
            output = transform.GetChild(i);
            if (output.CompareTag(tag))
            {
                return output;
            }
        }

        return output;
    }

    public static bool GetRandomness(float probablity)
    {
        if (probablity >= 1f)
        {
            return true;
        } else if (probablity <= 0f)
        {
            return false;
        }
        
        float randomFloat = Random.Range(0, 13);
        if (randomFloat > 1 - probablity)
            return true;
        return false;
    }
    
    private static int RandomDirection()
    {
        return Random.Range(0, 3);
    }
    private static float RandomFloat(float min, float max)
    {
        return Random.Range(min, max);
        //double val = (_random.NextDouble() * (max - min) + min);
        //return (float) val;
    }

    private static Vector3 RandomInCameraBounds()
    {
        Vector3 outputVector = new Vector3();
        switch (RandomDirection())
        {
            case 0:
                outputVector = new Vector3(15.7777309f, 0.833895683f, -28.471426f);
                break;
            case 1:
                outputVector = new Vector3(55.617733f, 2.60489559f, -38.5814285f);
                break;
            case 2:
                outputVector = new Vector3(62.117733f, 2.60489559f, 8.23857307f);
                break;
                
        }

        return outputVector;
    }

    public static void SetSpawnLocation(GameObject spawnableObject, GameObject spawnAt = null)
    {
        if (spawnableObject == null)
            return;
        
        if (spawnAt == null)
        {
            spawnableObject.transform.position = RandomInCameraBounds();
        }
        else
        {
            spawnableObject.transform.position = spawnAt.transform.position;
        }
        
        
    }
    
    private static float GetRotationAngle(Vector3 rotationVector)
    {
        return Mathf.Atan2(rotationVector.y, rotationVector.x) * Mathf.Rad2Deg;
    }

    public static int GetRandomElement(params GameObject[] list)
    {
        return Random.Range(0, list.Length - 1);
    }

    public static bool ProbablityRandom(float probablity)
    {
        return Random.value <= probablity;
    }

    public static bool GameActiveSelf()
    {
        return EditorApplication.isPlayingOrWillChangePlaymode;
    }
    
}

