using System.Collections;
using UnityEngine;

namespace HiryuTK.TopDownController
{
    /// <summary>
    /// We will use this base class instead an interface (IPoolable) for the poolable
    /// prefabs, as the inspector cannot reference prefabs (via drag and drop) 
    /// using an interface type, and I want to avoid referencing a prefab by 
    /// GameObject type. The downside of doing this is that the prefab must 
    /// inherit from this base class instead of MonoBehaviour. 
    /// </summary>
    public abstract class PoolObject : MonoBehaviour
    {
        protected Pool pool;

        public virtual void InitialSpawn(Pool pool)
        {
            this.pool = pool;
        }

        public virtual void Activation(Vector2 p, Quaternion r) 
        {
            transform.position = p;
            transform.rotation = r;
        }

        protected virtual void Despawn()
        {
            pool.Despawn(this);
        }
    }
}