using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class evensystem : MonoBehaviour
{
    private Color fadeColor = Color.blue;
    public static evensystem Instance;
    public GameObject upgrade_cannon;
    public direction_canoon_2 d;
    public bool isBase;
    private void Awake()
    {
        Instance = this; 
        isBase= true;
    }
  
    public void _btn_newGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        manager_ui.instance.Open_panel_readdy_game();
        manager_ui.instance.Close_panel_gameOver();
        gameManager.Instance.new_game();
        manager_ui.instance.Close_panel_pause();
        Time.timeScale = 1;
    }
    public void _btn_delete()
    {
        d.isDestroy();
        _btn_Close_attack_upgrade();
    }
    public void _btn_attack_upgrade()
    {
        if(gameManager.Instance.SpendCurrency(d.upgrade_cost_damage) == true)
        {
            d.set_bulletDamage(1);
            d.upgrade_cost_damage += 10;
        }
        else
        {
            manager_ui.instance.txt_no_money.gameObject.SetActive(true);
            InvokeRepeating(nameof(not_enough_money), 0, 0.3f);
        }
        d.cannon_upgrade();
    }
    public void _btn_shoot_time_upgrade()
    {
        if (d.get_bullet_shoot_time() > 0.1f) {
            if (gameManager.Instance.SpendCurrency(d.upgrade_cost_shoot) == true)
            {
                d.set_bullet_shoot_time(0.01f);
                d.upgrade_cost_shoot += 10;
                Debug.Log(d.upgrade_cost_shoot + " -  " + d.get_bullet_shoot_time());
            }
            else
            {
                manager_ui.instance.txt_no_money.gameObject.SetActive(true);
                InvokeRepeating(nameof(not_enough_money), 0, 0.3f);
            }
        }
        else
        {
            manager_ui.instance.txt_no_money.gameObject.SetActive(true);
            InvokeRepeating(nameof(not_enough_money), 0, 0.3f);
        }
        d.cannon_upgrade();
    }
    public void not_enough_money()
    {
        fadeColor.a -= 0.1f;
        manager_ui.instance.txt_no_money.color = fadeColor;
        if (fadeColor.a <= 0)
        {
            CancelInvoke(nameof(not_enough_money));
            fadeColor.a = 1;
            manager_ui.instance.txt_no_money.gameObject.SetActive(false);
        }
    }
    public void _btn_pauseGame()
    {
        manager_ui.instance.Open_panel_pause();
        Time.timeScale = 0;
    }
    public void _btn_continue()
    {
        Time.timeScale= 1;
        manager_ui.instance.Close_panel_pause();
    }
    public void _btn_Close_attack_upgrade()
    {
        manager_ui.instance.Close_panel_upgrade();
        isBase = true;
    }
}
