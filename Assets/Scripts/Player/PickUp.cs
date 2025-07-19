using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

public class PickUp : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rig rightHandRig;
    [SerializeField] private Rig leftHandRig;

    GameObject ovelappedItem;
    [HideInInspector] public GameObject rightEquippedItem;
    [HideInInspector] public GameObject leftEquippedItem;

    public Transform rightHandSocket;
    public Transform leftHandSocket;

    public static PlayerInputHandler Instance { get; private set; }
    private float lerpRightElapsedTime = 0f;
    private bool isRightLerping = false;
    private float lerpLeftElapsedTime = 0f;
    private bool isLeftLerping = false;

    public void SetOverlappingItem(GameObject item)
    {
        ovelappedItem = item; // Store the overlapping item for later use
    }

    void OnEnable()
    {
        PlayerInputHandler.Instance.InteractRightAction.performed += OnInteractRight;
        PlayerInputHandler.Instance.InteractLeftAction.performed += OnInteractLeft;
    }

    void OnDisable()
    {

    }

    private void OnInteractRight(InputAction.CallbackContext context)
    {
        if (ovelappedItem != null)
        {
            rightEquippedItem = ovelappedItem;
            ovelappedItem = null;
            rightEquippedItem.GetComponent<SphereCollider>().enabled = false;
            rightEquippedItem.transform.SetParent(rightHandSocket);
            rightEquippedItem.transform.localPosition = Vector3.zero;
            rightEquippedItem.transform.localRotation = Quaternion.identity;
            isRightLerping = true;


        }
    }
    private void OnInteractLeft(InputAction.CallbackContext context)
    {
        if (ovelappedItem != null)
        {
            leftEquippedItem = ovelappedItem;
            ovelappedItem = null;
            leftEquippedItem.GetComponent<SphereCollider>().enabled = false;
            leftEquippedItem.transform.SetParent(leftHandSocket);
            leftEquippedItem.transform.localPosition = Vector3.zero;
            leftEquippedItem.transform.localRotation = Quaternion.identity;
            isLeftLerping = true;
        }
    }

    void Start()
    {
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the player object.");
        }
    }

    private void Update()
    {
        if (isRightLerping)
        {
            lerpRightElapsedTime += Time.deltaTime;
            rightHandRig.weight = Mathf.Lerp(0, 1, lerpRightElapsedTime * 2);
            if(rightHandRig.weight ==1)
            {
                isRightLerping = false;
                lerpRightElapsedTime = 0f;
            }
        }
        if (isLeftLerping)
        { 
            lerpLeftElapsedTime += Time.deltaTime;
            leftHandRig.weight = Mathf.Lerp(0, 1, lerpLeftElapsedTime * 2);
            if (leftHandRig.weight == 1)
            {
                isLeftLerping = false;
                lerpLeftElapsedTime = 0f;
            }
        }
    }



}
