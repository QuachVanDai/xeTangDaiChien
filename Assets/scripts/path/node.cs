
using UnityEngine;


public class node : MonoBehaviour
{
    public static node Instance;
    public bool canMoveLeft = false;
    public bool canMoveRight = false;
    public bool canMoveUp = false;
    public bool canMoveDown = false;
    public GameObject nodeLeft;
    public GameObject nodeRight;
    public GameObject nodeUp;
    public GameObject nodeDown;
    public string[] direction_s;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        int index = 0;
       direction_s = new string[100];
        for (int i = 0; i < gameManager.Instance.path.Length - 1; i++)
        {
            if (transform.position == gameManager.Instance.path[i].transform.position)
            {
                index = i + 1;
                break;
            }
        }

        RaycastHit2D hitDown;
        hitDown = Physics2D.Raycast(transform.position, -Vector2.up);
        //   Debug.Log(Vector2.Distance(gameManager.Instance.path[index].position, hitDown.collider.transform.position) +" "+index);
        if (hitDown.collider != null)
            if (hitDown.collider.tag == "node" && Vector2.Distance(gameManager.Instance.path[index].transform.position, hitDown.collider.transform.position) == 0)
            {
                canMoveDown = true;
                nodeDown = hitDown.collider.gameObject;
               direction_s[index] = "down";
                return;
            }

        RaycastHit2D hitUp;
        hitUp = Physics2D.Raycast(transform.position, Vector2.up);
        if (hitUp.collider != null)
            if (hitUp.collider.tag == "node" && Vector2.Distance(gameManager.Instance.path[index].transform.position, hitUp.collider.transform.position) == 0)
            {
                canMoveUp = true;
                nodeUp = hitUp.collider.gameObject;
                direction_s[index] = "up";
                return;
            }
        RaycastHit2D hitRight;
        hitRight = Physics2D.Raycast(transform.position, Vector2.right);
        if (hitRight.collider != null)
            if (hitRight.collider.tag == "node" && Vector2.Distance(gameManager.Instance.path[index].transform.position, hitRight.collider.transform.position) == 0)
            {
                canMoveRight = true;
                nodeRight = hitRight.collider.gameObject;
                direction_s[index] = "right";
                return;
            }
        RaycastHit2D hitLeft;
        hitLeft = Physics2D.Raycast(transform.position, -Vector2.right);
        if (hitLeft.collider != null)
            if (hitLeft.collider.tag == "node" && Vector2.Distance(gameManager.Instance.path[index].transform.position, hitLeft.collider.transform.position) == 0)
            {
                canMoveLeft = true;
                nodeLeft = hitLeft.collider.gameObject;
               direction_s[index] = "left";
                return;
            }

    }


    public GameObject GetNodeFromDirection(string direction)
    {
        if (direction == "left" && canMoveLeft)
        {
            return nodeLeft;
        }
        else if (direction == "right" && canMoveRight)
        {
            return nodeRight;
        }
        else if (direction == "up" && canMoveUp)
        {
            return nodeUp;
        }
        else if (direction == "down" && canMoveDown)
        {
            return nodeDown;
        }
        else { return null; }
    }

    // Update is called once per frame
                void Update()
    {

              }





}
/*void Start()
{
    RaycastHit2D[] hitDown;
    //  transform.position = new Vector2(transform.position.x-0.05f,transform.position.y-0.05f);
    hitDown = Physics2D.RaycastAll(transform.position, -Vector2.up);
    for (int i = 0; i < hitDown.Length; i++)
    {
        float distance = Mathf.Abs(hitDown[i].point.y - transform.position.y);
        if (distance < 0.5f && hitDown[i].collider.tag == "node")
        {
            canMoveDown = true;
            nodeDown = hitDown[i].collider.gameObject;
        }
    }
    RaycastHit2D[] hitUp;
    hitUp = Physics2D.RaycastAll(transform.position, Vector2.up);
    for (int i = 0; i < hitUp.Length; i++)
    {
        float distance = Mathf.Abs(hitUp[i].point.y - transform.position.y);
        //            Debug.Log(distance + " " + hitUp[i].point.y + " " + transform.position.y);

        if (distance < 0.5f && hitUp[i].collider.tag == "node")
        {
            canMoveUp = true;
            nodeUp = hitUp[i].collider.gameObject;
        }
    }
    RaycastHit2D[] hitRight;
    hitRight = Physics2D.RaycastAll(transform.position, Vector2.right);
    for (int i = 0; i < hitRight.Length; i++)
    {
        float distance = Mathf.Abs(hitRight[i].point.x - transform.position.x);

        if (distance < 0.5f && hitRight[i].collider.tag == "node")
        {
            canMoveRight = true;
            nodeRight = hitRight[i].collider.gameObject;

        }
    }
    RaycastHit2D[] hitLeft;
    hitLeft = Physics2D.RaycastAll(transform.position, -Vector2.right);
    for (int i = 0; i < hitLeft.Length; i++)
    {
        float distance = Mathf.Abs(hitLeft[i].point.x - transform.position.x);

        if (distance < 0.5f && hitLeft[i].collider.tag == "node")
        {
            canMoveLeft = true;
            nodeLeft = hitLeft[i].collider.gameObject;
        }
    }

}*/