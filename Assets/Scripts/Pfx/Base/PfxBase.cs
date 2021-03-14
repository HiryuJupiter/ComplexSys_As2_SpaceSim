using UnityEngine;
using System.Collections;

public abstract class PfxBase : MonoBehaviour, IPoolable
{
    [SerializeField] private float timeBeforeDestroy = 1f;

    ParticleSystem ps;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    IEnumerator DelayBeforeDestroy()
    {
        yield return new WaitForSeconds(timeBeforeDestroy);
        Despawn();
    }

    public void InitialActivation(Pool pool)
    {
    }

    public void Activation()
    {
        ps.Play();
        StartCoroutine(DelayBeforeDestroy());
    }

    public void Reactivation(Vector3 pos)
    {

    }

    public void Reactivation(Vector3 pos, Quaternion rot)
    {
    }

    public void Despawn()
    {
    }
}