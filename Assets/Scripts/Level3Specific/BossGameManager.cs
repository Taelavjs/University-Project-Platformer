using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossGameManager : MonoBehaviour
{

    public PlayerHealth healthPlayer;
    public bossGuns bossGuns;

    public TextMeshProUGUI playerHealthTxt;
    public TextMeshProUGUI bossHealth;

    public GameObject btnRestart;


// Start is called before the first frame update
void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bossHealth.text = bossGuns.bossHp.ToString();
        playerHealthTxt.text = healthPlayer.health.ToString();

        if (bossGuns.bossHp <= 0)
        {
            btnRestart.SetActive(true);
        }
        if (healthPlayer.health <= 0)
        {
            SceneManager.LoadScene(3, LoadSceneMode.Single);

        }
    }
}
