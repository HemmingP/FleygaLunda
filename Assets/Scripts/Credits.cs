using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine;

public class Credits : MonoBehaviour
{

    [Header("ScrollSpeed")]
    [SerializeField] public Vector3 ScrollSpeed = new Vector3(0, 1f, 0);
    [Header("StartPos")]
    [SerializeField] public Vector3 StartPos = new Vector3(534.5f, -500.0f, 0f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // Press Escape to return to start menu
        if (Keyboard.current.escapeKey.wasPressedThisFrame || Gamepad.current?.buttonEast.wasPressedThisFrame == true)
        {
            Debug.Log("Escape.");
            SceneManager.LoadScene("StartScene");
        }
        transform.position += ScrollSpeed;

        if (transform.position.y > 800)
            transform.position = StartPos;
    }
}
