using UnityEngine;
using System.Collections;

public class TweezerTray : MonoBehaviour {
	
	void OnTriggerEnter(Collider other){
		Debug.Log("I found a: "+other.tag);
		BaseAttack baseAttack = other.GetComponent<BaseAttack>();
		if (baseAttack==null)Destroy(other.gameObject);
		else baseAttack.onToolSuccess();
	}
}
