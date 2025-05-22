using Unity.VisualScripting;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    // Zona de variables
    [Header("Añado ref del joystick a la variable")]
    [SerializeField] private Joystick joystick;
    private float horizontalAxis = 0f;
    private float verticalAxis = 0f;
    [SerializeField] private float speed = 2f;
   private Animator anim;

    #region UNITY METHODS
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }
    #endregion
    #region MY METHODS
    private void Move()
    {
        if (joystick.Horizontal >= 0.5f || joystick.Horizontal <= -0.5f)
            horizontalAxis = joystick.Horizontal * speed * Time.deltaTime;
        else
            horizontalAxis = 0f;

        if (joystick.Vertical >= 0.5f || joystick.Vertical <= -0.5f)
            verticalAxis = joystick.Vertical * speed * Time.deltaTime;
        else
            verticalAxis = 0f;

        if (horizontalAxis > 0f)
            RotateCharacter(0f, 90f, 0f);
        if (horizontalAxis < 0f)
            RotateCharacter(0f, -90f, 0f);
        if (verticalAxis > 0f)
            RotateCharacter(0f, 0f, 0f);
        if (verticalAxis < 0f)
            RotateCharacter(0f, 180f, 0f);

        transform.localPosition += new Vector3(horizontalAxis, 0f, verticalAxis);
        anim.SetFloat("inputVertical", joystick.Vertical);
        anim.SetFloat("inputHorizontal", joystick.Horizontal);            
    }

    private void RotateCharacter(float gradosX,float gradosY, float gradosZ)
    {
        transform.eulerAngles = new Vector3(gradosX, gradosY, gradosZ);
    }

    #endregion

}
