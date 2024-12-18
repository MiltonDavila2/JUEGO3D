using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZombieSpawnController : MonoBehaviour
{
    public  int initialZombiesPerWave = 5;
    public int currentZombiesPerWave;

    public float spawnDelay = 0.5f;

    public int currentWave  = 0;

    public float waveCooldown = 10.0f;

    public bool inCooldown;

    public float cooldownCounter =  0;

    public List<Zombie> currentZombiesAlive;

    public GameObject zombiePrefab;

    public TextMeshProUGUI titleWaveOverUI;

    public TextMeshProUGUI cooldownCounterTitleUI;

    public TextMeshProUGUI currentWaveUI;



    private void Start(){
        currentZombiesPerWave = initialZombiesPerWave;
        GlobalReferences.Instance.waveNumber = currentWave;
        StartNextWave();
    }

    private void StartNextWave(){

        currentZombiesAlive.Clear();
        currentWave++;
        GlobalReferences.Instance.waveNumber = currentWave;
        currentWaveUI.text = "Wave: "+currentWave.ToString();
        int waveSurvived = GlobalReferences.Instance.waveNumber;
        SaveLoadManager.Instance.SaveHighScore(waveSurvived);

        StartCoroutine(SpawnWave());
    }


    private IEnumerator SpawnWave(){
        for(int i = 0 ; i<currentZombiesPerWave; i++){
            Vector3 spawnOffset = new Vector3(Random.Range(-1f,1f),0f,Random.Range(-1,1f));
            Vector3 spawnPosition = transform.position + spawnOffset;

            var zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
            Zombie enemyScript = zombie.GetComponent<Zombie>();

            currentZombiesAlive.Add(enemyScript);

            yield return new WaitForSeconds(spawnDelay);

        }
        
    }


    private void Update(){
        List<Zombie> zombiesToremove = new List<Zombie>();

        foreach(Zombie zombie in currentZombiesAlive){
            if(zombie.isDead){
                zombiesToremove.Add(zombie);
            }
        }



        foreach(Zombie zombie in zombiesToremove){
            currentZombiesAlive.Remove(zombie);
        }


        zombiesToremove.Clear();

        if(currentZombiesAlive.Count == 0 && inCooldown ==  false){
            StartCoroutine(WaveCooldown());
        }

        if(inCooldown){
            cooldownCounter -= Time.deltaTime;

        }else{
            cooldownCounter = waveCooldown;
        }

        cooldownCounterTitleUI.text = cooldownCounter.ToString("F0"); 
    }

    private IEnumerator WaveCooldown(){
        inCooldown = true;

        titleWaveOverUI.gameObject.SetActive(true);


        yield return new WaitForSeconds(waveCooldown);

        inCooldown = false;
        titleWaveOverUI.gameObject.SetActive(false);

        currentZombiesPerWave *= 2;

        StartNextWave();
    }


}
