using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraControl : MonoBehaviour
{
    public Canvas pauseCanvas;
    public static bool showingPopUp = false;
    public static bool paused;

    // Script used for the ingame camera control
    void Update()
    {
        pause(SceneManager.GetActiveScene().name);
    }
    // move the camera by pressing the arrow keys
    private void move()
    {
        var yPosition = transform.position.y;
        var oldPosition = transform.position;
        if (Input.GetKey("up") && Camera.main.transform.position.x >= -250)
        {
            transform.Translate(new Vector3(0, 0, Time.deltaTime * 100f));
        }
        if (Input.GetKey("down"))
        {
            transform.Translate(new Vector3(0, 0, -1 * Time.deltaTime * 100f));
        }
        if (Input.GetKey("left"))
        {
            transform.Translate(new Vector3(-1 * Time.deltaTime * 100f, 0, 0));
        }
        if (Input.GetKey("right"))
        {
            transform.Translate(new Vector3(Time.deltaTime * 100f, 0, 0));
        }
        if (transform.position.x >= -250 && transform.position.x <= 170 && transform.position.z >= -200 && transform.position.z <= 250)
        {
            transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
        } else
        {
            transform.position = oldPosition;
        }
    }

    private void rotate()
    {
        // rotate camera right and left by pressing spacebar and moving the cursor to the edge of the screen
        if (Input.mousePosition.x >= Screen.width * 0.95 )//&& Input.GetKey("space"))
        {
            transform.RotateAround(transform.position, Vector3.up, 1.0f);
        }
        if (Input.mousePosition.x <= Screen.width * 0.05 )//&& Input.GetKey("space"))
        {
            transform.RotateAround(transform.position, Vector3.down, 1.0f);
        }
        // rotate camera right and left by pressing spacebar and pageup/pagedown
        if (Input.GetKey("page up") && Input.GetKey("space"))
        {
            transform.RotateAround(transform.position, Vector3.down, 1.0f);
        }
        if (Input.GetKey("page down") && Input.GetKey("space"))
        {
            transform.RotateAround(transform.position, Vector3.up, 1.0f);
        }
        // rotate camera up and down by pressing spacebar and moving the cursor to the edge of the screen
        if (Input.mousePosition.y >= Screen.height * 0.95 && Input.GetKey("space") && (Camera.main.transform.localRotation.eulerAngles.x >= 40))
        {
            transform.Rotate(Vector3.left, 1.0f);
        }
        if (Input.mousePosition.y <= Screen.height * 0.05 && Input.GetKey("space") && (Camera.main.transform.localRotation.eulerAngles.x <= 85))
        {
            transform.Rotate(Vector3.right, 1.0f);
        }
        // scroll in- and out using the mouse scrollwheel
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Camera.main.transform.position.y < 120)
        {
            transform.Translate(new Vector3(0, 0, -1 * Time.deltaTime * 600f));
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.transform.position.y > -15)
        {
            transform.Translate(new Vector3(0, 0, Time.deltaTime * 600f));
        }
    }

    private void pause(string sceneName)
    {
        // press escape to pause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseCanvas.isActiveAndEnabled)
            {
                paused = true;
                Time.timeScale = 0;
                pauseCanvas.enabled = true;
            }
            else
            {
                paused = false;
                Time.timeScale = 1;
                pauseCanvas.enabled = false;
            }

        }
        if (!paused)
        {
            if(sceneName != "Consumer" && sceneName != "Producer")
            {
                move();
                rotate();
            }
        }
        if (paused)
        {
            transform.RotateAround(transform.position, Vector3.up, 0.1f);
        }
    }
}