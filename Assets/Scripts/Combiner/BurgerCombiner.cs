using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class BurgerCombiner : MonoBehaviour
{
    [SerializeField] private BurgerFoodsSO burgerFoods;
    
    [Header("Connection")]
    [SerializeField] private Transform connectionPoint;
    [SerializeField] private float distBetweenConnections;

    private List<Food> objects = new List<Food>();
    private bool isStarts = false;

    private void FixedUpdate() {
        StartCoroutine(RemoveCells());
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Food") {
            if (other.TryGetComponent<XRGrabInteractable>(out XRGrabInteractable interactable) & interactable.isSelected) { return; }

            Food food = other.GetComponent<Food>();

            if (food.isCooked & !food.isOvercooked & burgerFoods.acceptableFoods.Contains(food.food) & isStarts) {
                objects.Add(food);

                Rigidbody rig = food.GetComponent<Rigidbody>();
                rig.freezeRotation = true;
                rig.constraints = RigidbodyConstraints.FreezeAll;

                food.transform.position = connectionPoint.position + connectionPoint.up*(distBetweenConnections*objects.Count);
                food.transform.rotation = connectionPoint.rotation;

                food.transform.parent = connectionPoint;
            } else if (food.isCooked & !food.isOvercooked & burgerFoods.startFood == food.food) {
                objects.Add(food);
                isStarts = true;

                Rigidbody rig = food.GetComponent<Rigidbody>();
                rig.freezeRotation = true;
                rig.constraints = RigidbodyConstraints.FreezeAll;
                food.transform.position = connectionPoint.position;
                food.transform.rotation = connectionPoint.rotation;

                food.transform.parent = connectionPoint;
            } else if (food.isCooked & !food.isOvercooked & burgerFoods.endFood == food.food & isStarts) {
                objects.Add(food);

                food.transform.position = connectionPoint.position + transform.up*(distBetweenConnections*objects.Count);
                food.transform.rotation = connectionPoint.rotation;

                food.transform.parent = connectionPoint;

                Transform createdObj = Instantiate(connectionPoint).transform;
                createdObj.localScale = connectionPoint.parent.lossyScale;
                createdObj.position = connectionPoint.position;

                for (int i = 0; i < createdObj.childCount; i++) {
                    Transform child = createdObj.GetChild(i);

                    if (child.tag == "Food") {
                        Destroy(child.GetComponent<Food>());
                        Destroy(child.GetComponent<Rigidbody>());
                        Destroy(child.GetComponent<Collider>());
                    }
                }

                for (int i = 0; i < objects.Count; i++) {
                    Food obj = objects[i];
                    objects[i] = null;

                    Destroy(obj.gameObject);
                }

                StartCoroutine(RemoveCells());
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Food") {
            if (other.TryGetComponent<XRGrabInteractable>(out XRGrabInteractable interactable) & !interactable.isSelected) { return; }

            Food food = other.GetComponent<Food>();

            if (objects.Contains(food)) { 
                objects.Remove(other.GetComponent<Food>()); 

                food.transform.parent = null;

                Rigidbody rig = food.GetComponent<Rigidbody>();
                rig.freezeRotation = false;
                rig.constraints = RigidbodyConstraints.None;

                for (int i = 0; i < objects.Count; i++) {
                    objects[i].transform.position = connectionPoint.position + connectionPoint.up*(distBetweenConnections*i);
                }
            }
        }
    }

    public IEnumerator RemoveCells() {
        yield return 0;

        objects.RemoveAll(item => item == null);
    }

    private void OnDrawGizmosSelected() {
        for (int i = 0; i < 5; i++) {
            Gizmos.DrawWireSphere(connectionPoint.position + connectionPoint.up*(distBetweenConnections*i), 0.005f);
        }
    }
}
