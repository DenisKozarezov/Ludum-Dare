using Core.Units.State;

namespace Core.Units
{
    public class EnemyView : UnitView, IEnemy
    {
        protected override void Start()
        {
            if (UnitData == null) return;
            Taunt(PlayerView.Instance);
        }
    }
}