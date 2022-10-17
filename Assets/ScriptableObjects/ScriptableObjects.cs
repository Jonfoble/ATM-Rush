using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CollectablesScriptableObject", order = 1)]

public class ScriptableObjects : ScriptableObject
{
	public string collectableName;
	public Color color;
	public int spawnRate;
	public int value;
}
