using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerControls input;

    private void Awake() 
    {
        input = new PlayerControls();
    }

    private void OnEnable() 
    {
        input.Enable();
    }

    private void OnDisable() 
    {
        input.Disable();
    }

    
}
