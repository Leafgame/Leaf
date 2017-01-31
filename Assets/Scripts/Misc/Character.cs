using UnityEngine;

namespace Assets.Scripts.Misc
{
    public class Character : MonoBehaviour
    {
        private int MovementSpeed
        {
            get;
            set;
        }

        public virtual void Move()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Update()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Start()
        {
            throw new System.NotImplementedException();
        }

    }
}

