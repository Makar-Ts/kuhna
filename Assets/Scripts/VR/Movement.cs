using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(LineRenderer))]
public class Movement : MonoBehaviour
{
    public LayerMask mask;
    public LayerMask maskStop;
    public GameObject PointPrefab;
    [NonSerialized] public RaycastHit ray;
    [NonSerialized] public bool collided = false;
    [NonSerialized] public bool enable = false;
    private LineRenderer line;
    private Vector2 axisL, axisR;
    private bool teleportationEnd = true;
    private Vector3 teleportationPoint;
    private GameObject pointObject;
    private Transform rightHandTransform, leftHandTransform;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;

        rightHandTransform = GlobalManager.rightHandController.transform;
        leftHandTransform = GlobalManager.leftHandController.transform;
    }

    void Update()
    {
        axisL = InputManager.GetVector2(XRNode.LeftHand, CommonUsages.primary2DAxis);
        axisR = InputManager.GetVector2(XRNode.RightHand, CommonUsages.primary2DAxis);
        
        if (axisL.y > 0 || axisR.y > 0) {
            RaycastHit hit = new RaycastHit();
            Vector3[] pos;

            if (axisL.y > 0) {
                collided = Physics.Raycast(leftHandTransform.position, leftHandTransform.forward, out hit, 100, mask);
                //collided = raycastParabolTrajectory(leftHandTransform.position, leftHandTransform.forward, 20, 0.1f, mask, -0.4f, 0, out RaycastHit hit, out List<Vector3> points);            
                
                //line.positionCount = points.Count;
                
                pos = new Vector3[]{leftHandTransform.position, hit.point};
            } else {
                collided = Physics.Raycast(rightHandTransform.position, rightHandTransform.forward, out hit, 100, mask);
                
                pos = new Vector3[]{rightHandTransform.position, hit.point};
            }

            enable = true;

            if (collided && hit.normal == new Vector3(0, 1, 0) && !((maskStop & (1 << hit.collider.gameObject.layer)) != 0)) {
                if (pointObject) {
                    pointObject.transform.position = hit.point;
                    pointObject.transform.rotation = Quaternion.FromToRotation(transform.forward, hit.normal) * Quaternion.Euler(90, 0, 0);
                } else {
                    pointObject = Instantiate(PointPrefab, hit.point, Quaternion.FromToRotation(transform.forward, hit.normal) * Quaternion.Euler(90, 0, 0));
                }
                
                line.SetPositions(pos);
                line.enabled = true;

                teleportationPoint = hit.point;
                teleportationEnd = false;

                ray = hit;
            } else if (collided) {
                line.SetPositions(pos);
                line.enabled = true;

                if (pointObject) Destroy(pointObject);
                teleportationEnd = true;

                ray = hit;
            } else {
                line.enabled = false;

                if (pointObject) Destroy(pointObject);
                teleportationEnd = true;
            }
        } else {
            line.enabled = false;
            if (pointObject) Destroy(pointObject);

            if (!teleportationEnd) {
                transform.position = teleportationPoint;
                teleportationEnd = true;
            }
            collided = false;
            enable = false;
        }
    }

    bool raycastParabolTrajectory(Vector3 origin, Vector3 originDirection, float maxLength, float step, LayerMask mask, float coefA, float coefB, out RaycastHit hit, out List<Vector3> points) {
        bool collided = false;
        float offset = 0;
        Vector3 nextOrigin = origin;
        points = new List<Vector3>();
        Physics.Raycast(nextOrigin, new Vector3(0, 1, 0), out hit, 0, 0);
        coefB = originDirection.y*10;
        print(originDirection.y);

        while (!collided && offset < maxLength) {
            points.Add(nextOrigin);
            offset += step;

            Vector3 offsetDir = new Vector3(step, coefA*Mathf.Pow(offset, 2)+coefB*offset, step);
            offsetDir.y = offsetDir.y;
            offsetDir.x *= originDirection.x;
            offsetDir.z *= originDirection.z;

            float length = Vector3.Distance(nextOrigin, nextOrigin+offsetDir);

            collided = Physics.Raycast(nextOrigin, Vector3.Normalize(offsetDir), out hit, length, mask);

            nextOrigin += offsetDir;
        }

        return collided;
    }
}
