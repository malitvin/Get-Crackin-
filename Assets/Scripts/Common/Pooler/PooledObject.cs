//Unity
using UnityEngine;

namespace Common.Pooler
{
    public class PooledObject : MonoBehaviour
    {
        private GenericPooler pooler;

        public void SetPooler(GenericPooler pooler)
        {
            this.pooler = pooler;
        }

        public GenericPooler GetPooler()
        {
            return pooler;
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Remove()
        {
            gameObject.SetActive(false);
        }
    }
}
