using UnityEngine;
using UnityEngine.UI; // For Button functionality

public class SwitchObjects : MonoBehaviour
{
    public GameObject objectToEnable;
    public GameObject objectToDisable;
    public char switchButton;

    void Start()
    {
        // Make sure you've assigned objects in the inspector
        if (objectToEnable == null || objectToDisable == null)
        {
            Debug.LogError("Please assign objects in the inspector!");
        }

        objectToDisable.SetActive(false);
        // Subscribe to the button click event
        if (Input.GetKey(KeyCode.UpArrow))
        {
            OnButtonClick();
        }
    }

    void OnButtonClick()
    {
        // Toggle object visibility
        objectToEnable.SetActive(!objectToEnable.activeSelf);
        objectToDisable.SetActive(!objectToDisable.activeSelf);
    }
}