
using UnityEngine;
using UnityEngine.Events;

public class enemy_move : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [Header("Attributes")]
    [SerializeField] private float speed = 2f;

    [Header("Event")]
    public static UnityEvent onGameOver = new UnityEvent();

    private Transform target;
    private int pahtIndex = 0;
    private void Awake()
    {
        onGameOver.AddListener(gameOver);
    }
    public void gameOver()
    {
        manager_ui.instance.Open_panel_gameOver();
        gameManager.Instance.isplayGame = false;
        enemy_spawn.onEnemyReset.Invoke();
        Destroy(gameObject);
        return;
    }
    // Start is called before the first frame update
    void Start()
    {
        target = gameManager.Instance.path[pahtIndex].transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pahtIndex++;
              if (pahtIndex == gameManager.Instance.path.Length)
                {
                    enemy_spawn.onEnemyDestroys.Invoke();
                    if (--gameManager.Instance.quantity_blood > 0)
                    {
                        manager_ui.instance.set_txt_blood(gameManager.Instance.quantity_blood);
                    }
                    
                        if (gameManager.Instance.quantity_blood <= 0)
                        {
                            gameOver();
                        }
                    
                    Destroy(gameObject);
                    return;
                }
                else
                {
                    target = gameManager.Instance.path[(int)pahtIndex].transform;

                    GameObject n_gameObject = gameManager.Instance.path[(int)pahtIndex - 1];
                    node n_node = n_gameObject.GetComponent<node>();
                    if (n_node.canMoveRight == true)
                    {
                        transform.localRotation = Quaternion.Euler(0, 0, 270);

                    }
                    else if (n_node.canMoveLeft == true)
                    {
                        transform.localRotation = Quaternion.Euler(0, 0, 90);

                    }
                    else if (n_node.canMoveUp == true)
                    {
                        transform.localRotation = Quaternion.Euler(0, 0, 0);

                    }
                    else if (n_node.canMoveDown == true)
                    {
                        transform.localRotation = Quaternion.Euler(0, 0, 180);
                    }
                }
        }
    }
    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }
}
