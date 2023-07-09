
using UnityEngine;

public class buildManager : MonoBehaviour
{
    public static buildManager instances;

    [Header("references")]
    [SerializeField] private class_tank[] TankPrefabs;
    [SerializeField] private SpriteRenderer[] SP_tank;
    private int currentSelectTank = -1;

    private void Start()
    {
        currentSelectTank= 0;
        SP_tank[0].color = Color.white;
        SP_tank[1].color = Color.white;
        SP_tank[2].color = Color.white;
        SP_tank[3].color = Color.white;
    }
    private void Awake()
    {
        instances = this;
    }
    public class_tank GetSelectTank()
    {
        if (currentSelectTank == -1) return null;
        return TankPrefabs[currentSelectTank];
    }

    public void setSelectedTank(int selectTank)
    {
        currentSelectTank = selectTank;
        if (selectTank == 0)
        {
            SP_tank[0].color = Color.red;
            SP_tank[1].color = Color.white;
            SP_tank[2].color = Color.white;
            SP_tank[3].color = Color.white;

        }
        else if (selectTank == 1)
        {
            SP_tank[0].color = Color.white;
            SP_tank[1].color = Color.red;
            SP_tank[2].color = Color.white;
            SP_tank[3].color = Color.white;
        }
        else if (selectTank == 2)
        {
            SP_tank[0].color = Color.white;
            SP_tank[1].color = Color.white;
            SP_tank[2].color = Color.red;
            SP_tank[3].color = Color.white;
        }
        else if (selectTank == 3)
        {
            SP_tank[0].color = Color.white;
            SP_tank[1].color = Color.white;
            SP_tank[2].color = Color.white;
            SP_tank[3].color = Color.red;
        }
        else
        {
            SP_tank[0].color = Color.white;
            SP_tank[1].color = Color.white;
            SP_tank[2].color = Color.white;
            SP_tank[3].color = Color.white;
        }
    }
}
