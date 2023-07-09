using UnityEngine;
using UnityEngine.UI;

public class enemy_health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int blood=10;
    [SerializeField] private int currencyWorth=10;
    public static enemy_health Instance;

    public GameObject camera_;
    public Image fill_bar;
    int tmp_blood;
    
    private void Start()
    {
        camera_ = GameObject.Find("Main Camera");
        set_blood(Mathf.RoundToInt(blood * Mathf.Pow(enemy_spawn.Instance.currentWave, 0.75f)));
    }
    public void update_blood(int currency_blood, int max_blood)
    {
        fill_bar.fillAmount =(float) currency_blood/ (float)max_blood;
    }
   
    public void takeDamage(int damage)
    {
        blood -= damage;
        update_blood(blood,tmp_blood);
        if (blood==0)
        {
            gameManager.Instance.increaseCurrency(currencyWorth);
            enemy_spawn.onEnemyDestroys.Invoke();
            Destroy(gameObject);
            return;
        }
        else if (blood < 0)
        {
            if (blood + damage == 0)
            {
                return;
            }
            else if (blood + damage > 0)
            {
                gameManager.Instance.increaseCurrency(currencyWorth);
                enemy_spawn.onEnemyDestroys.Invoke();
                Destroy(gameObject);
                return;
            }
            else if (blood + damage < 0)
            {
 
                return;
            }

        }
    }
    private void Awake()
    {
        Instance = this;
    }
    public void set_blood(int number)
    {
        blood = number;
        tmp_blood = blood;
    }
}
