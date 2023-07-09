using System.Collections;

using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance;
    public Transform startpoint;
    public GameObject[] path;
    public int quantity_blood=10;
    public int currency;
    public int  time_Ready_to_play = 3;
    public bool isplayGame;

    private float timeGame_m;
    private float tmp_timeGame_m;
    private int tmp_currency;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        new_game();
    }
    public void new_game()
    {
        isplayGame = false;
        tmp_timeGame_m = timeGame_m;
        tmp_currency = currency;
        currency = 0;
        tmp_timeGame_m = 0;
        time_Ready_to_play = 3;
        quantity_blood = 10;
        InvokeRepeating(nameof(ready_to_play), 0, 1);
        CancelInvoke(nameof(timeGameOver));
    }

    public void ready_to_play()
    {
        if (isplayGame == true)
        {
            start_play();
            InvokeRepeating(nameof(timeGameOver), 0, 1);
        }
        else
        {
            StartCoroutine(set_time_ready_to_play());
        }
    }
    public IEnumerator set_time_ready_to_play()
    {
        manager_ui.instance.txt_time_ready_to_play.text = time_Ready_to_play.ToString();
        time_Ready_to_play -= 1;
        if (time_Ready_to_play < 0)
        {
            manager_ui.instance.txt_time_ready_to_play.text = "GO";
            CancelInvoke(nameof(ready_to_play));
            yield return new WaitForSeconds(1.5f);
            isplayGame = true;
            ready_to_play();
        }
    }
    public void start_play()
    {
        currency = tmp_currency;
        manager_ui.instance.set_txt_blood(quantity_blood);
        manager_ui.instance.Close_panel_readdy_game();
    }
    public void increaseCurrency(int amount)
    {
        currency += amount;
    }
    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void timeGameOver()
    {
        int hour = Mathf.FloorToInt(tmp_timeGame_m / 3600f);
        int minutes = Mathf.FloorToInt(tmp_timeGame_m / 60f);
        int seconds = Mathf.FloorToInt(tmp_timeGame_m - minutes * 60f);
        string timerString = string.Format("{0:00}:{1:00}:{2:00}", hour, minutes, seconds);
        manager_ui.instance.txt_time_game.text = timerString;
        tmp_timeGame_m += 1;
       
    }
}
