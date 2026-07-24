using Assets.Scripts.Command;
using Assets.Scripts.Entity;

namespace Assets.Scripts.State
{
    internal sealed class EnemyActionState : IState
    {
        public EnemyActionState(IStateController controller, EnemyEntity enemy, PlayerEntity target, ActionResolveState actionResolveState)
        {
            _controller = controller;
            _enemy = enemy;
            _target = target;
            _actionResolveState = actionResolveState;
        }

        // PlayerCommandStateと相互参照になるため、生成後に配線する。
        public void Configure(PlayerCommandState playerCommandState)
        {
            _playerCommandState = playerCommandState;
        }

        public void Init()
        {
            var command = new EnemyAttackCommand(_enemy, _target);
            _actionResolveState.Begin(command, _playerCommandState);
            _controller.ChangeState(_actionResolveState);
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }

        private readonly IStateController _controller;
        private readonly EnemyEntity _enemy;
        private readonly PlayerEntity _target;
        private readonly ActionResolveState _actionResolveState;

        private PlayerCommandState _playerCommandState;
    }
}
