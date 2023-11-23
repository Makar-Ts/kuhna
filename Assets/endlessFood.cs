using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endlessFood : MonoBehaviour
{
    int foodCount = 0;
    public GameObject foodPref;
    public Transform foodAnchor;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Food") {
            foodCount++;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Food") {
            foodCount--;

            if (foodCount == 0) Instantiate(foodPref, foodAnchor.position, foodAnchor.rotation);
        }
    }
}
