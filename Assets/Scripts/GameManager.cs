using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool hitStun;
    public float hitstunTime;
    public GameObject balloon;
    public float bufferInput;

    private enum State
    {
        Loading,
        Playing,
        PlayerRespawn,
        Paused,
    }

    public EnemyRespawnManager respawningEnemiesContainer;

    private State state;
    public PlayerMovement playerMovement;

    public int killCombo;

    // Start is called before the first frame update
    void Start()
    {
        hitStun = false;
        scene = SceneManager.GetActiveScene();


    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Loading:
                break;
            case State.Playing:
                break;
            case State.PlayerRespawn:
                playerMovement.RespawnPlayer();
                respawningEnemiesContainer.triggerRespawn();
                setStatePlaying();
                break;
            case State.Paused:
                break;
        }
    }

    private IEnumerator playerHitStun()
    {
        hitStun = true;
        yield return new WaitForSeconds(hitstunTime);
        hitStun = false;
    }

    public void triggerHitstun()
    {
        StartCoroutine("playerHitStun");
    }

    public void SetStateRespawn()
    {
            state = State.PlayerRespawn;
        Debug.Log(state);

    }

    public void setStatePlaying()
    {
        state = State.Playing;
    }

    
    public void increaseKillCombo()
    {
        killCombo++;
    }

    public void resetKillCombo()
    {
        killCombo = 0;
    }

    public int getKillCombo()
    {
        return killCombo;
    }

    Scene scene;
    int currentScene;
    // Start is called before the first frame update


    public void nextLevel()
    {
        SceneManager.LoadScene(currentScene + 1, LoadSceneMode.Single);
    }

}
