using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    [SerializeField] private LayerMask m_WhatIsGround;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;
    private NinjagirlMovement player;

    ObjectPooler objectPooler;


    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<NinjagirlMovement>();
        objectPooler = ObjectPooler.SharedInstance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && player.health>0)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        objectPooler.SpawnFromPool("Bullet", firePoint.position, firePoint.rotation);
    }
}
