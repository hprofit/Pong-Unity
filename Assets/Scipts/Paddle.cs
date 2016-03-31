using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

    // Simple storage for which player this is, left or right (flips which controls to use)
    public bool isLeftPlayer = false;
    public float velocity = 5.0f;
    // Direction is stored publicly so that Ball.cs can access it
    public Vector3 direction;
    // Represents the state of the user's input, whether they have moved the paddle or not
    private bool isMoving = false;
    // The paddle is permanently locked on the x and y axis
    private Vector3 axis;
    // The paddle is permanently locked to it's starting rotation
    private Quaternion rotation;

	// Use this for initialization
	void Start () {
        direction = Vector3.zero;
        axis = transform.position;
        rotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        // Move the paddle according to it's direction and velocity
        Vector3 deltaDirection = direction * velocity * Time.deltaTime;
        transform.Translate(deltaDirection);

        transform.position = new Vector3(axis.x, axis.y, transform.position.z);
        transform.rotation = rotation;
    }

    /// <summary>
    ///     Sets direction to an "up" 
    /// </summary>
    void MoveUp() {
        direction = Vector3.forward;
    }

    /// <summary>
    ///     Sets direction to an "down"
    /// </summary>
    void MoveDown () {
        direction = Vector3.back;
    }

    /// <summary>
    ///     Sets direction to a zero Vector3
    /// </summary>
    void StopMovement () {
        direction = Vector3.zero;
    }

    /// <summary>
    ///     Given an Event object of EventType KeyDown, checks against the assigned movement keys
    ///     for either the "Left" or "Right" player
    /// </summary>
    /// <param name="e">Event with EventType of KeyDown</param>
    void HandleKeyPress (Event e) {
        if (isLeftPlayer) {
            if (e.keyCode == KeyCode.W) {
                MoveUp();
            }
            if (e.keyCode == KeyCode.S) {
                MoveDown();
            }
        }
        else {
            if (e.keyCode == KeyCode.UpArrow) {
                MoveUp();
            }
            if (e.keyCode == KeyCode.DownArrow) {
                MoveDown();
            }
        }
    }

    /// <summary>
    ///     Given an Event object of EventType KeyUp, checks against the assigned movement keys
    ///     for either the "Left" or "Right" player
    /// </summary>
    /// <param name="e">Event with EventType of KeyUp</param>
    void HandleKeyRelease (Event e) {
        if (isLeftPlayer) {
            if (e.keyCode == KeyCode.W || e.keyCode == KeyCode.S) {
                StopMovement();
            }
        }
        else {
            if (e.keyCode == KeyCode.UpArrow || e.keyCode == KeyCode.DownArrow) {
                StopMovement();
            }
        }
    }

    void OnGUI() {
        Event e = Event.current;

        if (e.isKey) {
            if (e.type == EventType.KeyDown) {
                HandleKeyPress(e);
            }
            else if (e.type == EventType.KeyUp) {
                HandleKeyRelease(e);
            }
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Wall") {
            StopMovement();
            Debug.Log("Stop");
        }
    }
}
