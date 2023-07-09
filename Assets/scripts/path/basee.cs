
using UnityEngine;

public class basee : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    private GameObject Tank;
    private Color StartColor;
    private Color fadeColor = Color.blue;
    // Start is called before the first frame update    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartColor = sr.color;
       
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
       if (Tank != null) return;
        if (evensystem.Instance.isBase == true)
        {
            class_tank Tankbuild = buildManager.instances.GetSelectTank();
            if (Tankbuild == null) return;
            if (gameManager.Instance.SpendCurrency(Tankbuild._cost) == true)
            {
                Tank = Instantiate(Tankbuild._prefabs, transform.position, Quaternion.identity);
                buildManager.instances.setSelectedTank(-1);
            }
            else
            {
                manager_ui.instance.txt_no_money.gameObject.SetActive(true);
                InvokeRepeating(nameof(not_enough_money), 0, 0.3f);
            }
        }
    }

    public void not_enough_money()
    {
        fadeColor.a -= 0.1f;
        manager_ui.instance.txt_no_money.color = fadeColor;
        if (fadeColor.a <= 0)
        {
            CancelInvoke(nameof(not_enough_money));
            fadeColor.a=1;
            manager_ui.instance.txt_no_money.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
