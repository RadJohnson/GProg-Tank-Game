using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    private float mouseX;
    private float mouseY;
    [SerializeField]
    private Vector2 clampYaxis;

    public GameObject barrel;

    void Update()
    {
        mouseX += Input.GetAxisRaw("Mouse X");
        transform.localRotation = Quaternion.Euler(0, mouseX + 180, 0);

        mouseY -= Input.GetAxisRaw("Mouse Y");

        mouseY = Mathf.Clamp(mouseY, clampYaxis.x, clampYaxis.y);
        barrel.transform.localRotation = Quaternion.Euler(0, 0, mouseY);
    }
}
