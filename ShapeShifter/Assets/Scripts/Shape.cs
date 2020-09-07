using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    [Header("Shape Variables")]
    [Tooltip("The delay on the destruction of the shape")]
    [SerializeField]
    private float deathDelay = 1f;
    [Tooltip("The fall speed of the shape")]
    [SerializeField]
    private float fallSpeed = 1f;

    private ParticleSystem destroyParticle;
    private AudioSource destroySound;
    private bool isActive = true;

    private void Awake()
    {
        destroyParticle = gameObject.GetComponentInChildren<ParticleSystem>();
        destroySound = gameObject.GetComponentInChildren<AudioSource>();
    }

    private void Update()
    {
        if (isActive)
        {
            transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
            BoundaryCheck();
        }
    }
    private void BoundaryCheck()
    {
        //If the shape is lower than -6 on the Y axis
        if (gameObject.transform.position.y <= -6f)
        {
            //Destroy it
            Destroy(gameObject);
        }
    }

    public void DestroyShape()
    {
        StartCoroutine(DestructionCoroutine());
    }

    private IEnumerator DestructionCoroutine()
    {
        isActive = false;
        destroyParticle.Play();
        destroySound.Play();
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }
}
