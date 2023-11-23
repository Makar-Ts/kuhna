using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCookingAnimation : MonoBehaviour
{
    private const float healthBar_widthMultiplier = 15, healthBar_height = 20, healthBar_borderWidth = 4;

    [SerializeField] private Food main;
    
    [Header("Colors")]
    private Material startMat;
    private MeshRenderer rend;
    [SerializeField] private Gradient colors;

    [Header("Block")]
    [SerializeField] private Transform scalableObject;
    [SerializeField] private Vector2 start2EndScale;

    private void Start() {
        rend = GetComponent<MeshRenderer>();
        startMat = rend.materials[0];
        colors.colorKeys[0] = new(startMat.color, 0);
    }

    private void FixedUpdate()
    {
        scalableObject.parent.gameObject.SetActive(main.IsCooking());
        float scale = scaleBetween(main.GetCookingTime(), start2EndScale.x, start2EndScale.y, 0, main.food.timeToCook);
        scalableObject.localScale = new(scalableObject.localScale.x, (scale > start2EndScale.y ? start2EndScale.y : scale), scalableObject.localScale.z);

        Material mat = startMat;
        mat.color = colors.Evaluate(scaleBetween(main.GetCookingTime(), 0, 1, 0, main.food.timeToOvercooked));
        print(scaleBetween(main.GetCookingTime(), 0, main.food.timeToOvercooked, 0, 1));

        rend.materials[0] = mat;
    }

    float scaleBetween(float value, float outMin, float outMax, float inputMin, float inputMax) =>
        outMin + (outMax - outMin) * ((value - inputMin) / (inputMax - inputMin));
}
