using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float timer = 0.5f;
    private void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = true;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject); 
    }

}
