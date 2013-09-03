using UnityEngine;
using System.Collections;
public class CameraShake : MonoBehaviour
{
   private Vector3 originPosition;
   private Quaternion originRotation;
   private float shake_decay;
   private float shake_power;
	private float startTime;
 
   void Update (){
      if (shake_power > 0){
         transform.position = originPosition + Random.insideUnitSphere * shake_power;
         transform.rotation = new Quaternion(
         originRotation.x + Random.Range (-shake_power,shake_power) * .2f,
         originRotation.y + Random.Range (-shake_power,shake_power) * .2f,
         originRotation.z + Random.Range (-shake_power,shake_power) * .2f,
         originRotation.w + Random.Range (-shake_power,shake_power) * .2f);
         shake_power -= shake_decay * Time.deltaTime;
      }
   }
 
   public void shake(){
	originPosition = transform.position;
	originRotation = transform.rotation;
	shake_power = .1f;
	shake_decay = 0.1f;
   }
	
	public void shakeStrong(float duration){
	originPosition = transform.position;
	originRotation = transform.rotation;
	shake_power = .2f;
	shake_decay = (shake_power/duration);
		Invoke("stopShakeAfter",duration);
		startTime = Time.time;
   }
	
	public void stopShakeAfter(){
		float timeDiff = Time.time-startTime;
		Debug.Log("Im stopping shaking"+timeDiff.ToString());
	}
}