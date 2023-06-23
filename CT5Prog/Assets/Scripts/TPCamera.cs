using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TPCamera : MonoBehaviour
{
    public GameObject player;

    public float minDstFromPlayer;
    public float maxDstFromPlayer;

    public Vector3 playerPivotOffset;

    private float mouseX;
    private float mouseY;

    [SerializeField]
    private Vector2 clampYaxis;

    PhotonView view;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if(view.IsMine)
        {
            mouseX += Input.GetAxisRaw("Mouse X");
            mouseY -= Input.GetAxisRaw("Mouse Y");
            mouseY = Mathf.Clamp(mouseY, clampYaxis.x, clampYaxis.y);
        }
        //Vector3 targetRotation = new Vector3(mouseY,mouseX+90);//90 is needed so thta the camera starts behind the player
        //transform.eulerAngles = targetRotation;

        //transform.position = (player.transform.position + playerPivotOffset) - transform.forward * maxDstFromPlayer;

        //player.transform.rotation = Quaternion.Euler(0, mouseX, 0);//rotates the player to face way the camera moves (may want to make this trigger when move void is triggered(so that player woudl stay still while camera orbits))


    }

    private void LateUpdate()
    {
        if (view.IsMine)
        {
            Vector3 targetRotation = new Vector3(mouseY, mouseX + 90);//90 is needed so thta the camera starts behind the player
            transform.eulerAngles = targetRotation;

            transform.position = (player.transform.position + playerPivotOffset) - transform.forward * maxDstFromPlayer;
        }
    }   

    //need to make it so the camera cant clip through ground

}