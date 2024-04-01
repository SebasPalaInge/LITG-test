using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2f;
    [SerializeField] private InputManager inputMovement;
    private CharacterController charControl;
    private Vector3 movementVector = Vector3.zero;

    private void Start()
    {
        charControl = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        //Get Input from input manager
        Vector2 _input = inputMovement.input.MovementActions.Forward.ReadValue<Vector2>();
        //Vectors for altering player transform by camera look
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float speedX = playerSpeed * _input.x;
        float speedY = playerSpeed * _input.y;

        movementVector = (right * speedX) + (forward * speedY);

        charControl.Move(movementVector * Time.deltaTime);
    }
}
