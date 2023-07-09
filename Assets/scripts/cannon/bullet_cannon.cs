using UnityEngine;
public class bullet_cannon : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private Color bulletColor;

    public static bullet_cannon Instance;
    private int damage_bullet;
    Transform target;
    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }
    public void set_color(Color c)
    {
        bulletColor = c;
    }
    public void set_damage_bullet(int number)
    {
        this.damage_bullet = number;
    }
    public void settarget_firingPoint(Transform _target)
    {
        target = _target;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "tag_enemy")
        {
            collision.gameObject.GetComponent<enemy_health>().takeDamage(damage_bullet);
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (target != null)
        {
           GetComponent<SpriteRenderer>().color = bulletColor;
            Vector2 direction = (target.transform.position - transform.position).normalized;
            rb.velocity = direction * bulletSpeed;
        }
    }
    
  
    private void Update()
    {
        Destroy(gameObject, 0.25f);
    }
}

/*public static bullet_cannon instance;
float gettime;
Transform target;
public GameObject bulletPrefab;
public Transform gunPosition;
public Color bulletColor;
public float bulletSpeed;
bool is_can_shoot = false;
public float dame;
private void Awake()
{
    if (instance == null) { instance = this; }
}
void Start()
{
    gettime = Time.time;

}
void Update()
{
    if (Time.time - gettime > 0.1f)
    {
        if (is_can_shoot == true && target != null)
        {

            Vector3 Position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
            Vector3 direction = Position - gunPosition.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            gunPosition.rotation = Quaternion.Euler(0, 0, angle);
            GameObject bulletClone = Instantiate(bulletPrefab);
            bulletClone.transform.position = gunPosition.position;
            bulletClone.transform.rotation = Quaternion.Euler(0, 0, angle);
            bulletClone.GetComponent<Rigidbody2D>().velocity = gunPosition.right * bulletSpeed;
            bulletClone.GetComponent<SpriteRenderer>().color = bulletColor;
        }
        gettime = Time.time;
    }
}
private void OnTriggerStay2D(Collider2D collision)
{
    if (collision.tag == "enemy")
    {

        target = collision.gameObject.transform;
        is_can_shoot = true;

    }
}
private void OnTriggerExit2D(Collider2D collision)
{
    if (collision.tag == "enemy")
    {

        is_can_shoot = false;
    }
}*/