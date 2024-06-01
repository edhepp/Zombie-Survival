using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior2 : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IDamageable>().TakeDamage(25.0f);
        Destroy(this.gameObject);
    }
}
