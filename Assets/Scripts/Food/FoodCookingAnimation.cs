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
    private float colorScale = 0;
    [SerializeField] private Gradient colors;

    [Header("Block")]
    [SerializeField] private Transform scalableCookedObject;
    [SerializeField] private Transform scalableOvercookedObject;
    [SerializeField] private Vector2 start2EndScale;

    private void Awake() {
        if (main == null) {
            if (!TryGetComponent<Food>(out main)) {
                this.enabled = false;
            }
        }

        if (scalableCookedObject == null & scalableOvercookedObject == null) {
            scalableCookedObject = transform.GetChild(0).GetChild(1);
            scalableOvercookedObject = transform.GetChild(0).GetChild(2);
        }

        rend = GetComponent<MeshRenderer>();
        startMat = rend.materials[0];
        colors.colorKeys[0] = new(startMat.color, 0);
    }

    private void FixedUpdate()
    {
        if (main != null) {
            if (scalableCookedObject) {
                scalableCookedObject.parent.gameObject.SetActive(main.IsCooking());
                scalableCookedObject.parent.rotation = Quaternion.identity;

                float scale = scaleBetween(main.GetCookingTime(), start2EndScale.x, start2EndScale.y, 0, main.food.timeToCook);
                if (!float.IsNaN(scale)) scalableCookedObject.localScale = new(scalableCookedObject.localScale.x, (scale > start2EndScale.y ? start2EndScale.y : scale), scalableCookedObject.localScale.z);
            }
            if (scalableOvercookedObject) {
                float scale = scaleBetween(main.GetCookingTime(), start2EndScale.x, start2EndScale.y, main.food.timeToCook, main.food.timeToOvercooked);
                if (!float.IsNaN(scale)) scalableOvercookedObject.localScale = new(scalableOvercookedObject.localScale.x, (scale > start2EndScale.y ? start2EndScale.y : scale < start2EndScale.x ? start2EndScale.x : scale), scalableOvercookedObject.localScale.z);
            }

            colorScale = scaleBetween(main.GetCookingTime(), 0, 1, 0, main.food.timeToOvercooked);
        }

        Material mat = startMat;
        mat.color = colors.Evaluate(colorScale);

        rend.materials[0] = mat;
    }

    float scaleBetween(float value, float outMin, float outMax, float inputMin, float inputMax) =>
        outMin + (outMax - outMin) * ((value - inputMin) / (inputMax - inputMin));
}
