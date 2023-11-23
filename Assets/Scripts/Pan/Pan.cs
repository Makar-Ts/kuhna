using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pan : MonoBehaviour
{
    [Header("Pan")]
    [SerializeField] private float tempMultiplier = 1f;
    [SerializeField] private float coolOffSpeed = 5f;

    [Header("Collider")]
    [SerializeField] private Vector3 center;
    [SerializeField] private Vector3 size;
    [SerializeField] private LayerMask mask;

    public void AddTemperature(float temp) {
        if (temperature < 40f) temperature += temp*tempMultiplier*Time.fixedDeltaTime; 
    }

    private RaycastHit[] cast;
    private float temperature = 0f;

    private void FixedUpdate() {
        if (temperature > 0f)temperature -= coolOffSpeed*tempMultiplier*Time.fixedDeltaTime;

        if (temperature < 30f) return;

        RaycastHit[] newCast = Physics.BoxCastAll(transform.position + center, size, transform.forward, Quaternion.identity, 10, mask);

        foreach (var item in newCast) {
            Food food = item.collider.GetComponent<Food>();

            food.Cooking();
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube(transform.position + center, size*2);
    }

    // private void OnGUI() {
    //     string text = "";
    //     for (int i = 0; i < cast.Length; i++) {
    //         text += " " + cast[i].collider.name + "\n";
    //     }

    //     GUI.Label(new Rect(new(0, 0), new(100, 100)), text);
    // }
}
