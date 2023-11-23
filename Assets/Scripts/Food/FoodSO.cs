using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodScriptableObject", menuName = "ScriptableObjects/FoodScriptableObject", order = 1)]
public class FoodSO : ScriptableObject
{
    public string foodName = "Food";
    public float timeToCook = 5f, timeToOvercooked = 10f;
}
