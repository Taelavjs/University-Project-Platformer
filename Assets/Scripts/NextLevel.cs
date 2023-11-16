
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextLevel : MonoBehaviour
{
    public Animator animator;
    Scene scene;
    int currentScene = 0;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startPresssed()
    {
        animator.SetTrigger("FadeIn");
    }
}
