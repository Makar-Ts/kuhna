using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public FoodSO food;
    public bool isCooked = false, isOvercooked = false;
    
    private float _cookingTime = 0f;
    private float _lastCookingTime = 0f;
    private bool _isCooking = false;

    private void Awake() {
        if (food.timeToCook == 0f) {
            isCooked = true;
        }
    }

    public void Cooking() {
        if (Time.time - _lastCookingTime >= 0.5f) _lastCookingTime = Time.time;

        _cookingTime += Time.time - _lastCookingTime;
        _lastCookingTime = Time.time;

        if (_cookingTime > food.timeToCook) {
            isCooked = true;
            if (_cookingTime > food.timeToOvercooked) {
                isOvercooked = true;
            }
        };
    }
}
