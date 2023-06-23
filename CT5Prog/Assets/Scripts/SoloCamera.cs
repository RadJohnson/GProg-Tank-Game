using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloCamera : MonoBehaviour
{
    public GameObject player;

    public float maxDstFromPlayer;

    public Vector3 playerPivotOffset;

    private float mouseX;
    private float mouseY;

    [SerializeField]
    private Vector2 clampYaxis;
    void Update()
    {
        mouseX += Input.GetAxisRaw("Mouse X");
        mouseY -= Input.GetAxisRaw("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, clampYaxis.x, clampYaxis.y);
    }

    
    void LateUpdate()
    {
        Vector3 targetRotation = new Vector3(mouseY, mouseX + 90);//90 is needed so thta the camera starts behind the player
        transform.eulerAngles = targetRotation;

        transform.position = (player.transform.position + playerPivotOffset) - transform.forward * maxDstFromPlayer;
    }
}
