using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(EntityAttributes))]
public class MovementController : MonoBehaviour
{
    //first person controller tutorial: https://www.youtube.com/watch?v=_QajrabyTJc


    public CharacterController CharacterController;

    public EntityAttributes Attributes; 

    public Vector2 Movement;
    public bool Jump;

    public float Gravity = -9.18f;

    Vector3 veloctity;
    bool isGrounded;


    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        Attributes = GetComponent<EntityAttributes>();
        Movement = Vector2.zero;
        isGrounded = true; 
    }

    // Update is called once per frame
    void Update()
    {
        GravityReset();

        //move around. 
        Vector3 moveDirection = transform.right * Movement.x + transform.forward * Movement.y;

        CharacterController.Move(moveDirection * Attributes.Speed * Time.deltaTime);


        //jumping
        if (Jump && isGrounded)
        {
            veloctity.y = Mathf.Sqrt(Attributes.JumpHeight * -2 * Gravity); 
            Jump = false;
        }

        //gravity
        veloctity.y += Gravity * Time.deltaTime; 

        CharacterController.Move(veloctity * Time.deltaTime);   


    }


    void GravityReset()
    {
        if (GroundCheck == null)
            return; 

        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if (isGrounded)
        {
            veloctity.y = -2f; 
        }
    }
}
