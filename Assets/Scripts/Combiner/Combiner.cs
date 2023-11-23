using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Combiner : MonoBehaviour
{
    [SerializeField] private CombinableFoodSO combinableFood;
    [SerializeField] private Transform spawnPoint;

    private List<Food> objects = new List<Food>();

    private void FixedUpdate() {
        StartCoroutine(RemoveCells());

        foreach (var item in combinableFood.products) {
            if (item.foods.Count != objects.Count) continue;
            
            bool same = true; 

            for (int i = 0; i < item.foods.Count; i++) {
                if (objects[i].food != item.foods[i]) {
                    same = false;
                    break;
                }
            }

            if (same) {
                for (int i = 0; i < objects.Count; i++) {
                    Food obj = objects[i];
                    objects[i] = null;

                    Destroy(obj.gameObject);
                }

                Instantiate(item.readyProduct, spawnPoint.position, spawnPoint.rotation);
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Food") {
            if (other.TryGetComponent<XRGrabInteractable>(out XRGrabInteractable interactable) & interactable.isSelected) { return; }

            Food food = other.GetComponent<Food>();

            if (food.isCooked & !food.isOvercooked) {
                objects.Add(food);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Food") {
            if (other.TryGetComponent<XRGrabInteractable>(out XRGrabInteractable interactable) & !interactable.isSelected) { return; }

            Food food = other.GetComponent<Food>();

            if (objects.Contains(food)) { objects.Remove(other.GetComponent<Food>()); }
        }
    }

    public IEnumerator RemoveCells() {
        yield return 0;

        objects.RemoveAll(item => item == null);
    }
}
