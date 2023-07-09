
using UnityEngine;
using UnityEngine.UI;

public class manager_ui : MonoBehaviour
{
    public static manager_ui instance;

    public Text txt_blood, txt_quantity_enemies, txt_cost,
        txt_time_game, txt_time_ready_to_play, txt_no_money,txt_level,
        txt_damage, txt_attack_upgrade_cost, txt_time_shoot,txt_time_shoot_upgrade_cost
        ;
    public GameObject panel_ready_to_play,panel_gameover,panel_upgrade,panel_pause;

    public void Close_panel_pause()
    {
        panel_pause.SetActive(false);
    }
    public void Open_panel_pause()
    {
        panel_pause.SetActive(true);
    }
    public void Close_panel_upgrade()
    {
        panel_upgrade.SetActive(false);
    }
    public void Open_panel_upgrade()
    {
        panel_upgrade.SetActive(true);
    }
    public void Close_panel_readdy_game()
    {
        panel_ready_to_play.SetActive(false);
    }
    public void Open_panel_readdy_game()
    {
        panel_ready_to_play.SetActive(true);
    }
    public void Open_panel_gameOver()
    {
        panel_gameover.SetActive(true);
    }
    public void Close_panel_gameOver()
    {
        panel_gameover.SetActive(false);
    }
    private void OnGUI()
    {
        txt_cost.text = gameManager.Instance.currency.ToString();
       
    }
    private void Awake()
    {
        instance = this;
    }
    public void set_txt_level(int number)
    {
        txt_level.text = "Level: " + number;
    }
    public void set_txt_quantity_enemies(int number1,int number2)
    {
        string formattedNumber = string.Format("{0:00}/{1:00}", number1, number2);

        txt_quantity_enemies.text = formattedNumber;
    }
    public void set_txt_blood(int number)
    {
        string formattedNumber = string.Format("{0:00}/{1:00}", number, 10);
        txt_blood.text = formattedNumber;
    }
}
