using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    Vector3 bulletDirection = new Vector3(0, 0, 1);
    [SerializeField] private float bulletSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(bulletDirection * bulletSpeed * Time.deltaTime);
    }
}
