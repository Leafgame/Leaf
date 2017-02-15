using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
    [RequireComponent(typeof(AudioSource))]
    public class ItemPickup : MonoBehaviour
    {
        public AudioClip PickupSound;
        public AudioSource AudioSource;

        private void Start()
        {
            AudioSource = GetComponent<AudioSource>();
        }
        

        protected void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Player")
            {
                AudioSource.PlayOneShot(PickupSound);  
                gameObject.SetActive(false);
            }
        }
    }
}
