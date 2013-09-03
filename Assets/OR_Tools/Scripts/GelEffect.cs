using UnityEngine;
using System.Collections;

public class GelEffect : MonoBehaviour {
	
	public float duration = 1.0f;
	// Use this for initialization
	void Start () {
		StartCoroutine(killme());
	}
	
	void OnTriggerEnter(Collider other)
	{
		//Debug.Log("GELTOUCHES:"+other.tag);
		if (other.tag.Equals("Gelable")){
			//dont destroy instead call enemy attack
			BaseAttack baseAttack = other.GetComponent<BaseAttack>();
			if (baseAttack==null)Destroy(other.gameObject);
			else
				baseAttack.onToolSuccess();
		}
	}
	 IEnumerator killme() {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
