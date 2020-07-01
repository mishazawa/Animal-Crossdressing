using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour {
    public float sensivityHor = 5f;
    public float sensivityVer = 3f;
    public float speed = 5f;
    public float limitRotationVer = 80f;

    private float rotationVer = 0f;

    void Start() {

    }

    void Update() {
        transform.Rotate(0, Input.GetAxis("Mouse X") * sensivityHor, 0);
        rotationVer -= Input.GetAxis("Mouse Y") * sensivityVer;
        rotationVer =  Mathf.Clamp(rotationVer, -limitRotationVer, limitRotationVer);
        transform.localEulerAngles = new Vector3(rotationVer, transform.localEulerAngles.y, 0);

        var dx = Input.GetAxis("Horizontal") * speed;
        var dz = Input.GetAxis("Vertical") * speed;

        var movement = new Vector3(dx, 0, dz);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);

        transform.position += Vector3.MoveTowards(Vector3.zero, movement, 10f);

        if (Input.GetKeyUp(KeyCode.R)) {
            transform.position = Vector3.zero;
        }

        if (Input.GetButton("Fire3")) {
            transform.position += Vector3.MoveTowards(Vector3.zero, Vector3.down, 10f);
        }

        if (Input.GetButton("Jump")) {
            transform.position += Vector3.MoveTowards(Vector3.zero, Vector3.up, 10f);
        }


    }
}
