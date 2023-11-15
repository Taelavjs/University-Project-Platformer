using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bossTrans;

    Rigidbody2D rbBullet;
    public GameObject bulletContainer;

    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public List<GameObject> list = new List<GameObject>();
    public void fireBullet(Transform pos)
    {
        var tmpObj = Instantiate(bulletPrefab, pos.position, Quaternion.identity);
        list.Add(tmpObj);

        rbBullet = tmpObj.GetComponent<Rigidbody2D>();

        Vector3 dir = (bossTrans.transform.position - gameObject.transform.position);

        rbBullet.velocity = dir * bulletSpeed;
    }

    public void allBulletsGone()
    {
        Debug.Log("WE ran");
        foreach (GameObject obj in list)
        {
            Destroy(obj);
        }
    }
}
