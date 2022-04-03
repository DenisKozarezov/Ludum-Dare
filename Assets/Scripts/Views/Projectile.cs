using UnityEngine;

namespace Core.Units
{
    public class Projectile : MonoBehaviour
    {
        [Header("Options")]
        [SerializeField, Min(0)]
        private float _speed;

        private UnitView _target;
       
        private void Update()
        {
            if (_target != null)
            {
                Vector2 direction = (_target.transform.position - transform.position).normalized;
                transform.Translate(direction * _speed * Time.deltaTime);
            }
        }
        private void OnTriggerEnter2D(Collider2D collider)
        {
            var unit = _target.GetComponent<UnitView>();
            if (unit == _target)
            {
                unit.Hit(10);
                Destroy(gameObject);
            }
        }

        public void Blast(UnitView target)
        {
            _target = target;
        }        
    }
}