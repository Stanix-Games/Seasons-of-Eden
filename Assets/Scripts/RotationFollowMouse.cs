using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFollowMouse : MonoBehaviour {

    Camera viewCamera;
    private float _angle;
    Vector3 mousePos;

    // Start is called before the first frame update
    void Start() {
        viewCamera = Camera.main;
    }

    // Update is called once per frame
    void Update () {
        HandlePlayerRotation();
    }

    private void HandlePlayerRotation() {
        FindMousePosition();
    }

    private void FindMousePosition() {
        mousePos = viewCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
    }

    public float angle { 
        get { return _angle; }
    }

}