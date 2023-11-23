using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BurgerFoodsScriptableObject", menuName = "ScriptableObjects/BurgerFoodsScriptableObject", order = 3)]
public class BurgerFoodsSO : ScriptableObject
{
    public FoodSO startFood, endFood;

    public List<FoodSO> acceptableFoods = new List<FoodSO>();
}

