using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    Vector2 axisLimitX = new Vector2(2.5f,122.5f);//2.5 bottom left 122.5 top right
    Vector2 axisLimitZ = new Vector2(2.5f,122.5f);//2.5 bottom left 122.5 top right

    float axisY = 30;


    public GameObject gm;
    private void Start()
    {

        Vector3 randomPos= new Vector3(Random.Range(axisLimitX.x,axisLimitX.y),axisY,Random.Range(axisLimitZ.x,axisLimitZ.y));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPos, Quaternion.identity);
        //if(gm.GetComponent<Gm>().playerInScene.Length %2 == 0)
        //{
        //    PhotonNetwork.Instantiate(playerPrefabs[0].name, randomPos, Quaternion.identity);
        //}
        //else
        //{
        //    PhotonNetwork.Instantiate(playerPrefabs[1].name, randomPos, Quaternion.identity);
        //}

    }



}
