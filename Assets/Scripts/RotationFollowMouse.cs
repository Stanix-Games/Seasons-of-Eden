using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFollowMouse : MonoBehaviour {
    Camera viewCamera;
    public float angle { get; private set; }
    public Vector3 mouseDelta { get; private set; }

    // Start is called before the first frame update
    void Start () {
        viewCamera = Camera.main;
    }

    // Update is called once per frame
    void Update () {
        UpdateAngle ();
    }

    private void UpdateAngle () {
        mouseDelta = viewCamera.ScreenToWorldPoint (Input.mousePosition) - transform.position;
        angle = Mathf.Atan2 (mouseDelta.y, mouseDelta.x) * Mathf.Rad2Deg;
    }
}
