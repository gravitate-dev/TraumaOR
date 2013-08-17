using UnityEngine;
using System.Collections;

//this guy will spawn enemy stuff every so often
public class LevelHandler : MonoBehaviour {
	
	public Transform area;
	public GameObject[] enemies = new GameObject[7];
	public float RateOfSpawn = 99;
	private float nextSpawn = 0;
	
	// Use this for initialization
	void Start () {
		if (RateOfSpawn<=0){
			Debug.LogError("RateOfSpawn: CANT SET TO ZERO!");
			RateOfSpawn = 5.0f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > nextSpawn)
        {
			nextSpawn = Time.time + RateOfSpawn;
			spawnEnemey(0);
		}
	}
	
	public void spawnEnemey( int type ) {
            // Random position within this transform
            Vector3 rndPosWithin;
            rndPosWithin = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            rndPosWithin = area.TransformPoint(rndPosWithin * .5f);
            Instantiate(enemies[type], rndPosWithin, area.rotation);      
	}
}
