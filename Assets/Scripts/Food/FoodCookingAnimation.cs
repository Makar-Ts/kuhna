using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCookingAnimation : MonoBehaviour
{
    private const float healthBar_widthMultiplier = 15, healthBar_height = 20, healthBar_borderWidth = 4;

    [SerializeField] private Food main;

    private Material startMat;
    private MeshRenderer rend;
    [SerializeField] private Gradient colors;

    private void Start() {
        rend = GetComponent<MeshRenderer>();
        startMat = rend.materials[0];
        colors.colorKeys[0] = new(startMat.color, 0);
    }

    private void Update()
    {
        Material mat = startMat;
        mat.color = colors.Evaluate(scaleBetween(main.GetCookingTime(), 0, 1, 0, main.food.timeToOvercooked));
        print(scaleBetween(main.GetCookingTime(), 0, main.food.timeToOvercooked, 0, 1));

        rend.materials[0] = mat;
    }

    private void OnGUI()
    {
        if (!main.IsCooking()) return;

        //GUI.skin = guiSkin;
        float initalHealth = main.food.timeToCook, currentHealth = main.GetCookingTime();
        if (initalHealth < currentHealth) currentHealth = initalHealth;

        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        screenPos.y = Screen.height - screenPos.y;
        screenPos -= new Vector2(initalHealth * healthBar_widthMultiplier / 2, healthBar_height * 2);

        Rect backgroundRect = new Rect(screenPos, new(initalHealth * healthBar_widthMultiplier + healthBar_borderWidth, healthBar_height));
        Rect healthRect = new Rect(screenPos + new Vector2(healthBar_borderWidth / 2, healthBar_borderWidth / 2), new(currentHealth * healthBar_widthMultiplier, healthBar_height - healthBar_borderWidth));

        GUI.color = Color.black;
        GUI.Box(backgroundRect, "");

        GUI.color = Color.green;
        GUI.Box(healthRect, "");
    }

    float scaleBetween(float value, float outMin, float outMax, float inputMin, float inputMax) =>
        outMin + (outMax - outMin) * ((value - inputMin) / (inputMax - inputMin));
}
