using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CubeSphereEditor : MonoBehaviour
{
    // The 5 cubes
    public GameObject[] cubes = new GameObject[5];

    // The 5 spheres
    public GameObject[] spheres = new GameObject[5];

    // Size of the cubes
    public float cubeSize = 1;

    // Size of the spheres
    public float sphereSize = 1;

    private void OnValidate()
    {
        // Changes all cube sizes
        foreach (GameObject cube in cubes)
        {
            if (cube != null)
            {
                cube.transform.localScale = Vector3.one * cubeSize;
            }
        }

        // Changes all sphere sizes
        foreach (GameObject sphere in spheres)
        {
            if (sphere != null)
            {
                sphere.transform.localScale = Vector3.one * sphereSize;
            }
        }
    }
}


#if UNITY_EDITOR

// Makes a custom inspector
[CustomEditor(typeof(CubeSphereEditor))]
public class CubeSphereCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Gets the script
        CubeSphereEditor manager = (CubeSphereEditor)target;

        // Updates the inspector
        serializedObject.Update();

        // Gets the variables
        SerializedProperty cubes = serializedObject.FindProperty("cubes");
        SerializedProperty spheres = serializedObject.FindProperty("spheres");
        SerializedProperty cubeSize = serializedObject.FindProperty("cubeSize");
        SerializedProperty sphereSize = serializedObject.FindProperty("sphereSize");

        // Shows cube list
        EditorGUILayout.PropertyField(cubes);

        // Shows sphere list
        EditorGUILayout.PropertyField(spheres);

        // Shows cube size
        EditorGUILayout.PropertyField(cubeSize);

        // Cube warning
        if (cubeSize.floatValue > 2)
        {
            EditorGUILayout.HelpBox(
                "The cubes' sizes cannot be bigger than 2!",
                MessageType.Warning
            );
        }

        // Shows sphere size
        EditorGUILayout.PropertyField(sphereSize);

        // Sphere warning
        if (sphereSize.floatValue < 1)
        {
            EditorGUILayout.HelpBox(
                "The spheres' sizes cannot be smaller than 1!",
                MessageType.Warning
            );
        }

        // Saves changes
        serializedObject.ApplyModifiedProperties();

        // Puts buttons next to each other
        EditorGUILayout.BeginHorizontal();

        // Selects all cubes
        if (GUILayout.Button("Select All Cubes"))
        {
            Selection.objects = manager.cubes;
        }

        // Selects all spheres
        if (GUILayout.Button("Select All Spheres"))
        {
            Selection.objects = manager.spheres;
        }

        EditorGUILayout.EndHorizontal();

        // Clears selection
        if (GUILayout.Button("Clear Selection"))
        {
            Selection.objects = new Object[0];
        }

        // Checks if cubes are enabled
        bool cubesEnabled = true;

        foreach (GameObject cube in manager.cubes)
        {
            if (cube != null && !cube.activeSelf)
            {
                cubesEnabled = false;
            }
        }

        // Saves old button color
        Color oldColor = GUI.backgroundColor;

        // Green if enabled, red if disabled
        GUI.backgroundColor = cubesEnabled ? Color.green : Color.red;

        // Enables or disables all cubes
        if (GUILayout.Button("Disable/Enable All Cubes"))
        {
            foreach (GameObject cube in manager.cubes)
            {
                if (cube != null)
                {
                    Undo.RecordObject(cube, "Toggle Cubes");

                    cube.SetActive(!cubesEnabled);
                }
            }
        }

        // Checks if spheres are enabled
        bool spheresEnabled = true;

        foreach (GameObject sphere in manager.spheres)
        {
            if (sphere != null && !sphere.activeSelf)
            {
                spheresEnabled = false;
            }
        }

        // Green if enabled, red if disabled
        GUI.backgroundColor = spheresEnabled ? Color.green : Color.red;

        // Enables or disables all spheres
        if (GUILayout.Button("Disable/Enable All Spheres"))
        {
            foreach (GameObject sphere in manager.spheres)
            {
                if (sphere != null)
                {
                    Undo.RecordObject(sphere, "Toggle Spheres");

                    sphere.SetActive(!spheresEnabled);
                }
            }
        }

        // Changes button color back
        GUI.backgroundColor = oldColor;
    }
}

#endif
