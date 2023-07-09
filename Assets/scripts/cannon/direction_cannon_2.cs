
using UnityEngine;
using UnityEngine.UI;

public class direction_canoon_2 : MonoBehaviour
{
 
    [Header("References")]
    [SerializeField] private Transform tankRotaionPoint; // 
    [SerializeField] private GameObject bulletprefabs;
    [SerializeField] private Transform firingPoint;

    [Header("Attributes")]
    [SerializeField] private float targetingRange=0;
    [SerializeField] private float radiuss=0;
    [SerializeField] private float rotationSpeed=0 ;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private float bullet_shoot_time = 0.21f;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private Color StartColor;
    public int upgrade_cost_damage=40;
    public int upgrade_cost_shoot=40;
    public Color bullet_color;
    private float getTime = 0;
    private Transform target;
    public void isDestroy()
    {
        Destroy(gameObject);
    }
    private void Start()
    {
        upgrade_cost_damage = 40;
         upgrade_cost_shoot = 40;
    }
    private void findTarget()
    {
         float radius = radiuss;
         float distance = targetingRange-1.3f;
         LayerMask enemyMask = LayerMask.GetMask("enemy");
         float numDirections = 90; // Số hướng
         float angleIncrement = 360f / numDirections; // Góc giữa các hướng
         for (int i = 0; i < numDirections; i++)
         {
             float angle = i * angleIncrement;
             Vector3 direction = Quaternion.Euler(0f, 0f, angle) * transform.right; // Xoay hướng theo góc
             RaycastHit2D[] hits= Physics2D.CircleCastAll(transform.position, radius, direction, distance, enemyMask);
             foreach (RaycastHit2D hit in hits)
             {
                Collider2D collider = hit.collider;
                float hitDistance = hit.distance;
                Vector3 origin = transform.position; // Đặt tọa độ gốc là vị trí của đối tượng hiện tại
                float angle1 = Mathf.Atan2(origin.y - hit.transform.position.y, origin.x - hit.transform.position.x) * Mathf.Rad2Deg;
                direction = Quaternion.Euler(0, 0, angle1)* (-transform.right);
                // Tính toán điểm kết thúc dựa trên vị trí va chạm
                Vector3 end = origin + direction * hitDistance;
            }
            if (hits.Length > 0)
             {
                 target = hits[0].transform;
                 return;
             }
         }
    }
  /*  private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetingRange - 1.3f);
    }*/
    private void rotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg-90;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        tankRotaionPoint.rotation = Quaternion.RotateTowards(tankRotaionPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    private bool checkTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }
    private void Shoot()
    { 
        GameObject bullet_Obj = Instantiate(bulletprefabs, firingPoint.position, Quaternion.identity);
        bullet_cannon bl = bullet_Obj.GetComponent<bullet_cannon>();
        bl.settarget_firingPoint(target);
        bl.set_damage_bullet(bulletDamage); 
        bl.set_color(bullet_color); 
    }
    void Update()
    {
         if (target == null)
         {
             findTarget();
             return;
         }
         rotateTowardsTarget();
         if (!checkTargetIsInRange())
         {
             target = null;
         }
         else
        {
             if (Time.time - getTime >= get_bullet_shoot_time())
             {
                 Shoot();
                getTime= Time.time;
            }
         }
    }
    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }
    private void OnMouseExit()
    {
        sr.color = StartColor;
    }
    private void OnMouseDown()
    {
        evensystem.Instance.d = GetComponent<direction_canoon_2>();
        manager_ui.instance.Open_panel_upgrade();
        evensystem.Instance.isBase = false;
        cannon_upgrade();
    }
    public void cannon_upgrade()
    {
        manager_ui.instance.txt_damage.text = "Sát thương " + bulletDamage + " +1 ";
        manager_ui.instance.txt_attack_upgrade_cost.text = "Chi phí " + upgrade_cost_damage + " xu.";
        manager_ui.instance.txt_time_shoot.text = "Thời gian " + get_bullet_shoot_time() + " - 0.01 ";
        manager_ui.instance.txt_time_shoot_upgrade_cost.text = "Chi phí " + upgrade_cost_shoot + " xu.";
    }
    public void set_bulletDamage(int number)
    {
        bulletDamage += number;
    }
    public void set_bullet_shoot_time(float number)
    {
        bullet_shoot_time -= number;
    }
    public float get_bullet_shoot_time()
    {
       return Mathf.Round(bullet_shoot_time * 10f) / 10f; 
    }
}
