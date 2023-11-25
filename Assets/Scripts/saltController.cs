using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saltController : MonoBehaviour
{
    [SerializeField] private ParticleSystem system;

    private void FixedUpdate() {
        float angle = Vector3.Angle(-Vector3.up, transform.forward);
        print(angle);

        if (angle <= 45) {
            if (system.isStopped) system.Play();
        } else {
            system.Stop();
        }
    }
}
