using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter1 : MonoBehaviour
{

    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] float shootDelay = 3f;

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
            //Todo: Centers offset doesn't work with rotations
            
            Instantiate(bulletPrefab, bulletSpawnPoint.position, 
                transform.rotation);
            SFXEventMediator.Instance?.PlayGunShotSFX();
            yield return new WaitForSeconds(shootDelay);
        }
    }
}
