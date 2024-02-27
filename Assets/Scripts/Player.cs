using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Movement
    private Vector3 playerMovementInput;
    private Vector2 playerMouseInput;
    private float mRotation;

    //SerializeFields
    [SerializeField] private Rigidbody playerBody;
    [SerializeField] private Transform playerFeet = null;
    [SerializeField] private Transform playerCamera;
    [Space]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeedMultiplier;
    [SerializeField] private float sensitivity;
    [SerializeField] private float jumpForce;

    //UI
    public InventoryObject inventory;
    public GameObject inventoryScreen;
    public GameObject mainHUD;
    public GameObject pauseMenu;
    
    //Raycasts
    RaycastHit pickUpItemRange;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement/Look(Camera)
        playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        playerMouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        MovePlayer();
        MovePlayerCamera();

        //Binds/Mechanics (PickUpObjects is on it's own separate script)
        PickUpItem();
        OpenClosePauseMenu();
        OpenCloseInventory();
        SaveLoadInventory();
    }

    //Movement - Function
    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(playerMovementInput) * walkSpeed;
        playerBody.velocity = new Vector3(MoveVector.x, playerBody.velocity.y, MoveVector.z);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            MoveVector = transform.TransformDirection(playerMovementInput) * walkSpeed * runSpeedMultiplier;
            playerBody.velocity = new Vector3(MoveVector.x, playerBody.velocity.y, MoveVector.z);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.OverlapSphere(playerFeet.position, 0.1f).Length == 1)
            {
                return;
            }
            playerBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    //Look(Camera) - Function
    private void MovePlayerCamera()
    {
        mRotation -= playerMouseInput.y * sensitivity;
        mRotation = Mathf.Clamp(mRotation, -90, 90);

        transform.Rotate(0f, playerMouseInput.x * sensitivity, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(mRotation, 0f, 0f);
    }

    //Pick up Items - Function
    private void PickUpItem()
    {
        if (Input.GetKey(KeyCode.E) && Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out pickUpItemRange, 3.75f))
        {
            var item = pickUpItemRange.collider.GetComponent<GroundItem>();
            if (item)
            {
                inventory.AddItem(new Item(item.item), 1);
                Destroy(pickUpItemRange.collider.gameObject);
            }
        }
    }

    //Open and Close Pause Menu - Function
    private void OpenClosePauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            mainHUD.SetActive(!mainHUD.activeSelf);
        }

        if (pauseMenu.active)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    //Open and Close Inventory Screen - Function
    private void OpenCloseInventory()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryScreen.SetActive(!inventoryScreen.activeSelf);
            mainHUD.SetActive(!mainHUD.activeSelf);
        }

        if (inventoryScreen.active)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    //Save and Load Inventory - Function
    private void SaveLoadInventory()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            inventory.Save();
        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
            inventory.Load();
        }
    }

    //Quit Game - Function
    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[14];
    }
}
