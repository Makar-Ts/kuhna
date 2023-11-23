using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endlessFood : MonoBehaviour
{
    int cnt = 0;
    public GameObject foodPref;
    public Transform foodAnchor;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ForFoodBox")
        {
            if (cnt == 0)
                Instantiate(foodPref, foodAnchor.position, foodAnchor.rotation);
        }
        else if (other.tag == "Food")
            cnt++;

    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Food")
            cnt--;
    }
}
