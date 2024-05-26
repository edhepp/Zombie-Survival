using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [SerializeField] GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BulletFiring());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator BulletFiring()
    {
        while(true)
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 0, 0.5f), Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }
}
