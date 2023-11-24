using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class InputManager : MonoBehaviour
{
    [SerializeField] private XRController rightHandController;
    [SerializeField] private XRController leftHandController;

    private void Start() {
        GlobalManager.leftHandController = leftHandController;
        GlobalManager.rightHandController = rightHandController;
    }

    public static bool GetBool(XRNode node, InputFeatureUsage<bool> button) {
        bool returned_val;
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(button, out returned_val);
        return returned_val;
    }

    public static float GetFloat(XRNode node, InputFeatureUsage<float> button) {
        float returned_val;
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(button, out returned_val);
        return returned_val;
    }

    public static Vector2 GetVector2(XRNode node, InputFeatureUsage<Vector2> button) {
        Vector2 returned_val;
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(button, out returned_val);
        return returned_val;
    }

    public static Vector3 GetVector3(XRNode node, InputFeatureUsage<Vector3> button) {
        Vector3 returned_val;
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(button, out returned_val);
        return returned_val;
    }
}
