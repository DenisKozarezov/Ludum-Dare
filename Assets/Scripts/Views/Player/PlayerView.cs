using Core.Units.State;

namespace Core.Units
{
    public class PlayerView : UnitView
    {
        private static PlayerView _instance;
        public static PlayerView Instance => _instance;

        protected override void Awake()
        {
            base.Awake();
            if (_instance == null) _instance = this;
        }
    }
}