using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public static PlayerInputHandler Instance { get; private set; }
    private InputSystem_Actions inputActions;
    public Vector2 MoveInput { get; private set; }

    public Vector2 LookInput { get; private set; }

    public InputAction InteractRightAction => inputActions.Player.InteractRight;
    public InputAction InteractLeftAction => inputActions.Player.InteractLeft;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        inputActions = new InputSystem_Actions();

      
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    

    // Update is called once per frame
    void Update()
    {
        MoveInput = inputActions.Player.Move.ReadValue<Vector2>();
        LookInput = inputActions.Player.Look.ReadValue<Vector2>();
    }
}
