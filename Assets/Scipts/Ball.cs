using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public float velocity = 1.0f;
    public Vector3 direction;
    public AudioClip bounceSound;

    private AudioSource source;
    
    // Use this for initialization
    void Start () {
        direction = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
        direction.Normalize();

        source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        // Move the ball according to it's direction and velocity
        Vector3 deltaDirection = direction * velocity * Time.deltaTime;
        transform.Translate(deltaDirection);
    }

    /// <summary>
    ///     Given a Vector3 representing the normal of whatever surface this ball
    ///     collided with, returns the previous direction vector mirrored around the normal
    /// </summary>
    /// <param name="wallNormal">Vector3 - Normal of surface the ball collided with</param>
    /// <returns>Vector3 - Mirror of the direction property around the given wallNormal</returns>
    Vector3 Bounce(Vector3 wallNormal) {
        float dot = Vector3.Dot(direction, wallNormal);
        Vector3 bouncedDireciton = direction - 2 * dot * wallNormal;
        return bouncedDireciton.normalized;
    }

    void PlayBounceSound() {
        source.PlayOneShot(bounceSound, 1.0f);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Wall") {
            foreach (ContactPoint contact in collision.contacts) {
                direction = Bounce(contact.normal);
                PlayBounceSound();
            }
        }
        else if (collision.gameObject.tag == "Paddle") {
            foreach (ContactPoint contact in collision.contacts) {
                direction = Bounce(contact.normal);
                PlayBounceSound();
            }
        }
    }
}
