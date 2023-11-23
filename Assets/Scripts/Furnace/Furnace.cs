using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour
{
    [SerializeField] private float tempSpeed;
    [SerializeField] private float rayLength;
    [SerializeField] private LayerMask mask;

    private void FixedUpdate() {
        Ray ray = new Ray(transform.position, transform.forward);
        
        if (Physics.Raycast(ray, out RaycastHit hit, rayLength, mask)) {
            hit.collider.GetComponent<Pan>().AddTemperature(tempSpeed);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawLine(transform.position, transform.position + rayLength*transform.forward);
    }
}
