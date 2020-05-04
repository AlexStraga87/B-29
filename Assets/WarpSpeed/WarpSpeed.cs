using UnityEngine;
using System.Collections;

public class WarpSpeed : MonoBehaviour {
	public float WarpDistortion;
	public float Speed;
	ParticleSystem particles;
	ParticleSystemRenderer rend;
	bool isWarping = true;

	void Awake()
	{
		particles = GetComponent<ParticleSystem>();
		rend = particles.GetComponent<ParticleSystemRenderer>();

        StartCoroutine(StretchCoroutine());
    }

    private IEnumerator StretchCoroutine()
    {
        WaitForFixedUpdate wait = new WaitForFixedUpdate();
        for (int i = 0; i < 50; i++)
        {
            rend.velocityScale = WarpDistortion * (Time.deltaTime * Speed * i);
            yield return wait;
        }
    }



}
