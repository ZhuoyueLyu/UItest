using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Data", menuName = "CreateData", order = 1)]
public class ScriptableObjectClass : ScriptableObject
{
    public string objectName = "New MyScriptableObject";
    public bool colorIsRandom = false;
    public Color thisColor = Color.white;
    public Vector3[] spawnPoints;
}