using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class UIScoreTextObjectPool
{
    List<GameObject> active = new List<GameObject>();
    List<GameObject> inactive = new List<GameObject>();
    GameObject prefab;
    RectTransform parent;

    //Constructor
    public UIScoreTextObjectPool(GameObject prefab, Canvas canvas)
    {
        this.prefab = prefab;
        parent = canvas.GetComponent<RectTransform>();

        if (prefab.GetComponent<IUITextPoolable>() == null)
        {
            Debug.LogWarning("Object is not a ScoreText");
        }
    }

    public GameObject Spawn(string text, Vector3 targetPosition)
    {
        GameObject p;
        if (inactive.Count > 0)
        {
            //If object pool is not empty, then take an object from the pool and make it active
            p = inactive[0];
            p.SetActive(true);
            inactive.RemoveAt(0);
        }
        else
        {
            //If object pool is empty, then spawn a new object.
            p = GameObject.Instantiate(prefab, targetPosition, Quaternion.identity, parent);
            p.GetComponent<IUITextPoolable>().SetUp(this, parent);
        }
        p.GetComponent<IUITextPoolable>().Activation(text, targetPosition);
        active.Add(p);
        return p;
    }

    public void Despawn(GameObject obj)
    {
        inactive.Add(obj);
        active.Remove(obj);
        obj.SetActive(false);
    }
}
