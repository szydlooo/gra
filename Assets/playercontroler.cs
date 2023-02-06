using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class playercontroler : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float acceleration = 10;
    private Rigidbody rb;
    private Vector2 controlls;
    private Transform gunleft, gunright;
    private bool fireButtonDown = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gunleft = transform.Find("gunleft");
        gunright = transform.Find("gunright");
    }

    // Update is called once per frame
    void Update()
    {
        float v, h;
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
       // if (v != 0 && h != 0)
        controlls = new Vector2(v, h);
        if (Mathf.Abs(transform.position.x) > 12)
        {
            Vector3 newPosition = new Vector3(transform.position.x * -1,
                0,
                transform.position.z);
                transform.position = newPosition;
        }
        if (Mathf.Abs(transform.position.z) > 9)
        {
            Vector3 newPosition = new Vector3(transform.position.x,
                0,
                transform.position.z * -1);
            transform.position = newPosition;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fireButtonDown = true;
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * controlls.y * acceleration, ForceMode.Acceleration);
        rb.AddTorque(transform.up * controlls.x * acceleration, ForceMode.Acceleration);
        {
            if (fireButtonDown)
            {
                GameObject bullet1 = Instantiate(bulletPrefab, gunleft.position, Quaternion.identity);
                bullet1.transform.parent = null;
                bullet1.GetComponent<Rigidbody>().AddForce(transform.forward * 10,
                                                            ForceMode.VelocityChange);
                Destroy(bullet1, 5);
                GameObject bullet2 = Instantiate(bulletPrefab, gunright.position, Quaternion.identity);
                bullet2.transform.parent = null;
                bullet2.GetComponent<Rigidbody>().AddForce(transform.forward * 10,
                                                            ForceMode.VelocityChange);
                Destroy(bullet2, 5);
                fireButtonDown = false;
            }
        }
    }
}
