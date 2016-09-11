﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour {

	//variables
	public List<DifficultyLevel> DifficultyLevels;

    //timer that calculates how much time has passed since last created
    private float spawnTimer;
    private float totalGameTime;

    //game difficulty times
    public float timeUntilMedium;
    public float timeUntilHard;

	private float currentPerSecondCheck;
	private DifficultyLevel currentDifficultyLevel;

	private void StoreNewPerSecondCheck()
	{
		currentPerSecondCheck = Random.Range(currentDifficultyLevel.PerSecondMin, currentDifficultyLevel.PerSecondMax);
	}

	private void SetCurrentDifficulty()
	{
		if (totalGameTime > timeUntilMedium && totalGameTime < timeUntilHard)
			currentDifficultyLevel = DifficultyLevels[1];
		else if (totalGameTime > timeUntilHard)
			currentDifficultyLevel = DifficultyLevels[2];
		else
			currentDifficultyLevel = DifficultyLevels[0];
	}

	// Use this for initialization
	void Start () {
		SetCurrentDifficulty();
		StoreNewPerSecondCheck();
	}
	
	// Update is called once per frame
	void Update () {

        spawnTimer += Time.deltaTime;
        totalGameTime += Time.deltaTime;

		SetCurrentDifficulty();

		if(spawnTimer > currentPerSecondCheck) 
		{
            spawnTimer = 0;
			spawnObstacle(currentDifficultyLevel);
			StoreNewPerSecondCheck();
        }
	}

    private void spawnObstacle(DifficultyLevel difficulty)
    {
		int getNewObstacle = Random.Range(0, difficulty.ObCollection.Count);

        GameObject newObstacle = Instantiate(difficulty.ObCollection[getNewObstacle], transform.position, Quaternion.identity) as GameObject;
   	}
}

[System.Serializable]
public class DifficultyLevel
{
	public string name;
	public float PerSecondMin;
	public float PerSecondMax;

	public List<GameObject> ObCollection;
}