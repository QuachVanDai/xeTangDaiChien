using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class enemy_spawn : MonoBehaviour
{
    [Header("references")]
    [SerializeField] private int baseEnemies = 10; // khởi tạo số lượng enemy

    [Header("Attributes")]
    [SerializeField] private GameObject[] enemyprefabs;  // mảng enemy
    [SerializeField] private GameObject bossenemyprefabs;  
    [SerializeField] private float timeBetweenWaves = 2f;    // khoảng thời gian ra từng đợt enemy
    [SerializeField] private float difficutyScalingFactor = 0.75f;  // 
    [SerializeField] private float timeSinceLastSpawn=0.5f; // thời gian tạo enemt clone

    
    [SerializeField] private int enemiesLeftToSpawn=0; // số enemy còn lại 
    [SerializeField] private bool isSpawning = false;

    [Header("Event")]
    public static UnityEvent onEnemyDestroys = new UnityEvent();
    public static UnityEvent onEnemyReset = new UnityEvent();

    public static enemy_spawn Instance;
    private int sum_enemy;
    public int currentWave = 1;
    private float getTime;
  
    private void Awake()
    {
        onEnemyDestroys.AddListener(EnemyDestroyed);
        onEnemyReset.AddListener(reset);
        Instance= this;
    }
    private void Start()
    {
        getTime = Time.time;
        StartCoroutine(StartWave());
    }
    private void EnemyDestroyed()
    {
        sum_enemy--;
        manager_ui.instance.set_txt_quantity_enemies(sum_enemy, enemyPerWaves());
    }
    public void reset()
    {
        difficutyScalingFactor = 0.75f;
        currentWave = 1;
        baseEnemies = 10;
        StartCoroutine(StartWave());
    }
    IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = enemyPerWaves();
        sum_enemy = enemyPerWaves();
        manager_ui.instance.set_txt_quantity_enemies(enemiesLeftToSpawn, enemyPerWaves());
        manager_ui.instance.set_txt_level(currentWave);

    }
    void EndWave()
    {
        isSpawning = false;
        currentWave++;
        manager_ui.instance.set_txt_level(currentWave);
        StartCoroutine(StartWave());
    }
    private int enemyPerWaves()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficutyScalingFactor));
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
    if (!isSpawning) { return; }
        if(gameManager.Instance.isplayGame==true)
        {
           
                if (Time.time - getTime >= timeSinceLastSpawn && enemiesLeftToSpawn > 0)
                {
                    SpawnEnemy();
                    getTime = Time.time;
                }
                if (enemiesLeftToSpawn == 0 && sum_enemy == 0)
                {
                    EndWave();
                }
            
        }
        
    }
    private void SpawnEnemy()
    {
        int index = UnityEngine.Random.Range(0,enemyprefabs.Length);
        if (currentWave % 5 == 0)
        {
            GameObject prefabsToSpaw = bossenemyprefabs;
            Instantiate(prefabsToSpaw, gameManager.Instance.startpoint.position, Quaternion.identity);
            enemiesLeftToSpawn=0;
            sum_enemy = 1;
            manager_ui.instance.set_txt_quantity_enemies(1, 1);
        }
        else if (currentWave % 5 != 0)
        {
            GameObject prefabsToSpaw = enemyprefabs[index];
            Instantiate(prefabsToSpaw, gameManager.Instance.startpoint.position, Quaternion.identity);
            enemiesLeftToSpawn--;
        }

    }
}
