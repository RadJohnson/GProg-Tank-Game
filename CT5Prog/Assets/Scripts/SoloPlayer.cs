using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloPlayer : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float moveSpeedMultiplier;

    private Vector2 movementAxis;

    private float movething = 50;

    private float rotationAxis;
    
    private Vector3 moveDirection;

    public float movementMultiplier = 50;

    public GameObject bullet;

    public GameObject shootPosition;

    public GameObject turret;
    public float projectileForce;

    public float shootDelay;
    private float timeBetweenShots;

    [SerializeField]
    private float rotateSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();


        //Application.targetFrameRate = 60;
        Application.targetFrameRate = -1;//sets it so that framrate is unlimited

        timeBetweenShots = shootDelay;

        currentHealth = maxHealth;

        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Move(Vector3 _moveDirection)
    {
        _moveDirection = (_moveDirection.normalized * (moveSpeed * moveSpeedMultiplier) * movething) * Time.deltaTime;

        rb.velocity = new Vector3(_moveDirection.x, rb.velocity.y, _moveDirection.z);
    }


    int currentHealth;
    public int maxHealth;

    public LayerMask layerToDamage;

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.layer == layerToDamage)
        {
            currentHealth -= 1;
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

    }


    private void Update()
    {
            mouseX = Input.GetAxisRaw("Mouse X");
            mouseY -= Input.GetAxisRaw("Mouse Y");

            movementAxis.y = Input.GetAxisRaw("Vertical");
            moveDirection = (transform.right * movementAxis.y + transform.forward * movementAxis.x);

            rotationAxis = Input.GetAxisRaw("Horizontal");

            Aiming();

            transform.Rotate(0, (rotationAxis * rotateSpeed) * Time.deltaTime, 0, Space.Self);

            turret.transform.Rotate(0, -(rotationAxis * rotateSpeed) * Time.deltaTime, 0, Space.Self);

            if (Input.GetButton("Fire1") && timeBetweenShots <= 0)
            {
                FireProjectile();
                timeBetweenShots = shootDelay;
            }
            else
            {
                timeBetweenShots -= Time.deltaTime;
            }

        
    }

    private void FixedUpdate()
    {
        Move(moveDirection);
    }

    void FireProjectile()
    {
        var tmp = GameObject.Instantiate(bullet, shootPosition.transform.position, shootPosition.transform.rotation);
        tmp.GetComponent<Rigidbody>().AddForce(-barrel.transform.right * projectileForce, ForceMode.Impulse);
    }

    private float mouseX;
    private float mouseY;
    [SerializeField]
    private Vector2 clampYaxis;

    public GameObject barrel;
    void Aiming()
    {
        turret.transform.Rotate(0, mouseX, 0, Space.Self);

        mouseY = Mathf.Clamp(mouseY, clampYaxis.x, clampYaxis.y);
        barrel.transform.localRotation = Quaternion.Euler(0, 0, mouseY);
    }
}