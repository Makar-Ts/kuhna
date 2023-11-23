using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CombinableFoodScriptableObject", menuName = "ScriptableObjects/CombinableFoodScriptableObject", order = 2)]
public class CombinableFoodSO : ScriptableObject
{
    [Serializable]
    public class Product {
        public GameObject readyProduct;
        public List<FoodSO> foods = new List<FoodSO>();
    }

    public List<Product> products = new List<Product>();
}
