
using UnityEngine;
using UnityEngine.SceneManagement;

public class FaderScript : MonoBehaviour
{
    public Animator animator;
    public float currentTime = 0f;
    public bool hasFaded = false;
    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.currentState == GameManager.GameState.GameOver && hasFaded == false)
        {
            FadeOut();
            hasFaded = true;
        }
        if (hasFaded == true)
        {
            if (currentTime < 1f)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                FadeIn();
            }
        }
    }

    public void FadeOut ()
    {
        animator.SetTrigger("FadeOut");
    }

    public void FadeIn()
    {
        animator.SetTrigger("FadeIn");
    }
    public void ChangeGameState(GameManager.GameState gamestate)
    {
        GameManager.instance.currentState = gamestate;
    }
}
