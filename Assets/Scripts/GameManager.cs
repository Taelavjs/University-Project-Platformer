using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool hitStun;
    public float hitstunTime;
    public GameObject balloon;

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

    public IEnumerator playerHitStun()
    {
        hitStun = true;
        yield return new WaitForSeconds(hitstunTime);
        hitStun = false;
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


}
