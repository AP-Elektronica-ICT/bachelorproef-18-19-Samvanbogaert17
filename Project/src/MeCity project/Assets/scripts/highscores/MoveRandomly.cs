using UnityEngine;

public class MoveRandomly : MonoBehaviour
{

    /*

    Vector3 target;
    public static bool hover = false;

    // script used to randomly move the fishes around
    private void Start()
    {
        // set hovering to true and get a random target to move to
        hover = true;
        target = new Vector3(Random.Range(-450, 450), Random.Range(-450, 450), 0);
        // randomly set the fish position and rotation towards the target
        transform.position = Vector3.MoveTowards(transform.position, target, 15f * 2000f);
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, target - transform.position, 15f, 0.0f));
        // get a new random target
        target = new Vector3(Random.Range(-450, 450), Random.Range(-450, 450), 0);
    }
    // when hovering on a fish, display the player/fish name and score
    private void OnMouseOver()
    {
        FindObjectOfType<addFish>().meganCanvas.enabled = false;
        if (hover)
        {
            FindObjectOfType<addFish>().canvas.enabled = true;
            var localFishes = FindObjectOfType<addFish>().fishes;
            for (int i = 0; i < localFishes.Count; i++)
            {
                if (localFishes[i].name == this.name)
                {
                    FindObjectOfType<addFish>().txtPlayer.text = this.name;
                    string test = FindObjectOfType<addFish>().lines[i];
                    string[] laatste = test.Split(':');
                    FindObjectOfType<addFish>().txtScore.text = laatste[1];
                }
            }
        }
    }
    // move the fish
    private void Update()
    {
        // check if the fish position is the same as the target position
        if ((int)(transform.position.x * 100) == (int)(target.x * 100) && (int)(transform.position.y * 100) == (int)(target.y * 100))
        {
            // if the position is the same, get a new target
            target = new Vector3((Random.Range(-450, 450)), (Random.Range(-450, 450)), 0);

            // check if the target stays within the screen
            if ((target.x < 500 && target.x > -500) && (target.y < 500 && target.y > -500))
            {
                // move the fish to the target
                transform.position = Vector3.MoveTowards(transform.position, target, 15f * Time.deltaTime);
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, target - transform.position, 15f, 0.0f));
            }
            else
            {
                // else get a new target
                target = new Vector3((Random.Range(-450, 450)), (Random.Range(-450, 450)), 0);
            }
        }
        // else move the fish and rotate the fish to the target
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 15f * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, target - transform.position, 15f, 0.0f));
        }
    }

    */

}