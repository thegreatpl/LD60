using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MovementController))]
public class PlayerController : MonoBehaviour
{

    public MovementController MovementController;

    //public ProjectileFire ProjectileFire; 

    public Camera Camera;

    public bool LockControls = false; 


    InputAction Move;
    InputAction Jump;
    InputAction Fire; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MovementController = GetComponent<MovementController>();
        //ProjectileFire = GetComponent<ProjectileFire>(); 
        Move = InputSystem.actions.FindAction("Move");
        Jump = InputSystem.actions.FindAction("Jump");
        Fire = InputSystem.actions.FindAction("Attack");

        //GetComponent<EntityAttributes>().OnDeath += () =>
        //{
        //    GameManager.Instance.GameOver(); 
        //}; 
    }

    // Update is called once per frame
    void Update()
    {
        if (LockControls)
            return; 

        Vector2 movement = Move.ReadValue<Vector2>();
        MovementController.Movement = movement;


       // MovementController.Jump = Jump.IsPressed();

        if (Fire.IsPressed())
        {
           // ProjectileFire.Fire(Camera.transform.forward); 
        }

    }
}
