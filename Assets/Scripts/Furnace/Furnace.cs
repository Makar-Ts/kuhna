using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour
{
    [SerializeField] private float tempSpeed;
    [SerializeField] private float rayLength;
    [SerializeField] private LayerMask mask;
    [SerializeField] private ParticleSystem gasEffect;

    private void FixedUpdate() {
        Ray ray = new Ray(transform.position, transform.forward);
        
        if (Physics.Raycast(ray, out RaycastHit hit, rayLength, mask)) {
            gasEffect.Play();
            hit.collider.GetComponent<Pan>().AddTemperature(tempSpeed);
        } else {
            gasEffect.Stop();
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawLine(transform.position, transform.position + rayLength*transform.forward);
    }
}
