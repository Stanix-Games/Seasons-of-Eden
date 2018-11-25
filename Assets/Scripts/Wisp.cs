using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : MonoBehaviour {
    public GameObject target;
    public float moveSpeed;
    public float rotationSpeed;

    Camera viewCamera;
    public float angle { get; private set; }
    public Vector3 mouseDelta { get; private set; }

    void Start () {
        viewCamera = Camera.main;
    }

    // Update is called once per frame
    void Update () {
        // Basically useless as wisp is child of player
        // transform.position = Vector3.MoveTowards (transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        // Vector3 vectorToTarget = target.transform.position - transform.position;
        // float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

        mouseDelta = viewCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        angle = Mathf.Atan2(mouseDelta.y, mouseDelta.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}