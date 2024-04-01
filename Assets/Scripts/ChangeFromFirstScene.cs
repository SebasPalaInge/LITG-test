using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeFromFirstScene : MonoBehaviour
{
    [SerializeField] private GameObject confirmAnimationPrompt;
    [SerializeField] private GameObject character;

    private void Start() 
    {
        DontDestroyOnLoad(character);    
    }
    
    public void StateConfirmAnimationPrompt(bool state)
    {
        confirmAnimationPrompt.SetActive(state);
    }

    public void ChangeToSceneTwo()
    {
        StateConfirmAnimationPrompt(false);
        SceneManager.LoadScene(1);
    }
}
