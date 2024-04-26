using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideCar : MonoBehaviour
{
    public Camera PlayerCamera; 
    public Camera carCamera;
    public PrometeoCarController carControls;
    public FirstPersonController firstPersonController;
    private bool isCarCameraActive = false;
    public GameObject player; // The player object (e.g., Armature)
    public GameObject Car; // The car object
    public float interactDistance = 4.0f; // Distance within which interaction is possible
    public KeyCode rideKey = KeyCode.F; // Key to ride the car
    public bool isRiding = false; // Whether the player is riding the car
    public Transform carSeat; // Transform to position player when riding

    void Update()
    {
        // Calculate distance between the player and the car
        float distance = Vector3.Distance(player.transform.position, Car.transform.position);
        

        // Check if within interaction distance and key is pressed to ride/dismount
        if (distance <= interactDistance && Input.GetKeyDown(rideKey) && !isRiding)
        {
            ToggleCamera();
            Ride();
        }
        else if (isRiding && Input.GetKeyDown(rideKey))
        {
            ToggleCamera();
            Dismount();
        }
    }
    void ToggleCamera()
    {
        // Toggle between the main camera and the car camera
        isCarCameraActive = !isCarCameraActive;

        if (PlayerCamera != null) PlayerCamera.enabled = !isCarCameraActive;
        if (carCamera != null) carCamera.enabled = isCarCameraActive;


    }

    void Start()
    {
        // Initialize the Rigidbody component

        
        firstPersonController.enabled = true;
        // Ensure car controls are disabled at start
        if (carControls != null) carControls.enabled = !firstPersonController.isActiveAndEnabled;


        // Ensure only one camera is active at start
        if (PlayerCamera != null) PlayerCamera.enabled = true;
        if (carCamera != null) carCamera.enabled = false;


    }
    
    void Ride()
    {
        isRiding = true;
        if (carControls != null) carControls.enabled = true ;
        if (firstPersonController != null) firstPersonController.enabled = false;
        // Move the player to the car seat position
        player.transform.position = carSeat.position;
        
        // Orient the player to match the car's rotation (ensures correct seating position)
        player.transform.rotation = carSeat.rotation;

        // Parent the player to the car to ensure they move with it
        player.transform.parent = Car.transform;

        

        
    }

    void Dismount()
    {
        isRiding = false;
        carControls.enabled = false;
        firstPersonController.enabled = true;
        // Detach the player from the car
        player.transform.parent = null;

        

        // Position the player slightly in front of the car when dismounting
        player.transform.position = Car.transform.position + Car.transform.forward * 2f;

        Debug.Log("Dismounting the Car");
    }
}