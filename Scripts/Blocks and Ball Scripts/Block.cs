using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour {

    
    private int count;
    public Text countText;
    private GameObject effectVfx;
    private AudioSource bounceSound;
    private ParticleSystem particleEffect;

	void Awake () {
        bounceSound = GameObject.Find("BounceSound").GetComponent<AudioSource>();
        transform.parent.tag = "Level";
        effectVfx = GameObject.Find("CFX3_Fire_Explosion");
        particleEffect = effectVfx.GetComponent<ParticleSystem>();
    }
  
    void Update () {
        
        if (transform.position.y <= -10)
            Destroy(gameObject);

	}

    public void SetStartingCount(int count)
    {
        this.count = count;
        countText.text = count.ToString();
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.collider.name == "Ball" && count > 0)
        {

            count--;
            Camera.main.GetComponent<CameraTransitions>().Shake();
            countText.text = count.ToString();
            bounceSound.Play();
            if (count == 0)
            {
                Destroy(gameObject);
                Camera.main.GetComponent<CameraTransitions>().MediumShake();
                GameObject.Find("ExtraBallProgress").GetComponent<Progress>().IncreaseCurrentWidth();
                // Instantiate(Resources.Load("CFX3_Fire_Explosion") as GameObject, transform.position, transform.rotation);
                effectVfx.transform.position = transform.position;
                particleEffect.Play();
            }
           
        }
    }

}
