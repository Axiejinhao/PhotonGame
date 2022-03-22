using UnityEngine;
using System.Collections;

public class triggerProjectile : MonoBehaviour {

	public GameObject projectile;
	public Transform shootPoint;
    public Transform targetPoint;///TODO:Need Set

    private GameObject magicMissile;
	public float attackCostTime = 1;///TODO:Need Set
    public float attackHeight = 12;///TODO:Need Set

    public GameObject hitEffect;


	public void shoot()
	{
		magicMissile = Instantiate(projectile, shootPoint.position, transform.rotation) as GameObject;	
		StartCoroutine(lerpyLoop(magicMissile));
	}

	// shoot loop
	public IEnumerator lerpyLoop(GameObject projectileInstance)
	{
        //var victim = targetPoint;//transform.position + transform.forward * attackRange;

		float progress = 0;
		float timeScale = 1.0f / attackCostTime;
		Vector3 origin = projectileInstance.transform.position;

        if (targetPoint == null)
            yield break;

		// lerp ze missiles!
		while (progress < 1)
		{
			if (projectileInstance && targetPoint)
			{			
    			progress += timeScale * Time.deltaTime;
    			float ypos = (progress - Mathf.Pow(progress, 2)) * attackHeight;
    			float ypos_b = ((progress + 0.1f) - Mathf.Pow((progress + 0.1f), 2)) * attackHeight;
    			projectileInstance.transform.position = Vector3.Lerp(origin, targetPoint.position, progress) + new Vector3(0, ypos, 0);
    			if (progress < 0.9f)
    			{
    				projectileInstance.transform.LookAt(Vector3.Lerp(origin, targetPoint.position, progress + 0.1f) + new Vector3(0, ypos_b, 0));
    			}
    			yield return null;
			}
		}

		Destroy(projectileInstance);

        if (hitEffect)
            Destroy(Instantiate(hitEffect, targetPoint.position, transform.rotation), 1.5f);

		yield return null;
	}

	public void clearProjectiles()
	{
		if (magicMissile)
			Destroy(magicMissile);
	}
}
