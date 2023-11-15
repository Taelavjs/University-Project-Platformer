using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bossGuns : MonoBehaviour
{
    public GameObject gunRotator;
    [System.Serializable]
    public class BossStages
    {
        public float rotateAmmount;
        public float timeBtwAttacks;
        public float originalTiming;
        public GameObject balloonArrangement;
    }

    [SerializeField]
    public List<BossStages> bossStages = new List<BossStages>();




    public int bossHp;


    public float bulletForce;


    private float hitDelay = 5f;
    public GunShoot[] gunShoot;
    public GameObject[] Guns;
    // Start is called before the first frame update
    void Start()
    {
        hitDelay = 0;

        bossStages[bossHp-1].balloonArrangement.SetActive(true);

        bossStages[bossHp-1].originalTiming = bossStages[bossHp-1].timeBtwAttacks;
    }

    // Update is called once per frame
    void Update()
    {
        if(bossHp > 0)
        {
            hitDelay -= Time.deltaTime;

            if (bossHp < 0)
            {
                gameObject.GetComponent<Animator>().SetTrigger("Dead");


            }

            bossStages[bossHp - 1].timeBtwAttacks -= Time.deltaTime;

            gunRotator.transform.Rotate(new Vector3(0f, 0f, -1f) * bossStages[bossHp - 1].rotateAmmount * Time.deltaTime);

            if (bossStages[bossHp - 1].timeBtwAttacks < 0 && hitDelay < 0)
            {
                foreach (GameObject gun in Guns)
                {
                    gun.GetComponent<GunShoot>().fireBullet(gun.transform);

                    Debug.Log("Roar");


                }

                bossStages[bossHp - 1].timeBtwAttacks = bossStages[bossHp - 1].originalTiming;

            }
        } 

    }

    public void isBossDead()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && hitDelay < 0){
            bossStages[bossHp - 1].balloonArrangement.SetActive(false);

            Debug.Log(gunShoot.Length);

            bossHp--;

            if(bossHp > 0) 
            {
                bossStages[bossHp - 1].balloonArrangement.SetActive(true);

            } else
            {
                gameObject.GetComponent<Animator>().SetTrigger("Dead");

            }
            foreach (GunShoot ob in gunShoot)
            {
                ob.allBulletsGone();

            }
            gameObject.GetComponent<Animator>().SetTrigger("TreeHit");
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.down * 10f;

            hitDelay = 5f;


        }
    }


}
