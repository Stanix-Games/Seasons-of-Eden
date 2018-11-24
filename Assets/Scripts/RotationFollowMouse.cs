using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFollowMouse : MonoBehaviour {

    Camera viewCamera;
    public float angle { get; private set; }

    private Vector3 mouseDelta;
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        viewCamera = Camera.main;
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateAngle();
        UpdatePlayerAnimation();
    }

    private void UpdateAngle()
    {
        mouseDelta = viewCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        angle = Mathf.Atan2(mouseDelta.y, mouseDelta.x) * Mathf.Rad2Deg;
    }

    private void UpdatePlayerAnimation()
    {
        Debug.Log(mouseDelta);
        playerAnimator.SetFloat("moveX", mouseDelta.x);
        playerAnimator.SetFloat("moveY", mouseDelta.y);
    }
}