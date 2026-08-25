using Assets.Scripts.Command;
using Assets.Scripts.Entity;

namespace Assets.Scripts.State
{
    /// <summary>
    /// 敵のターン。入場と同時に敵の行動を 1 つ組み立てて解決フェーズへ渡すため、
    /// この State に滞在するフレームは無い(<see cref="Update"/> は呼ばれない)。
    /// </summary>
    public sealed class EnemyActionState : IState
    {
        public EnemyActionState(IStateController controller, EnemyEntity enemy, PlayerEntity target, ActionResolveState actionResolveState)
        {
            _controller = controller;
            _enemy = enemy;
            _target = target;
            _actionResolveState = actionResolveState;
        }

        /// <summary>
        /// 解決後の遷移先を配線する。<see cref="PlayerCommandState"/> と相互参照になり
        /// コンストラクタでは解決できないため、生成後に呼ぶ。
        /// </summary>
        public void Configure(PlayerCommandState playerCommandState)
        {
            _playerCommandState = playerCommandState;
        }

        public void Init()
        {
            // 敵の行動は現状こうげき固定。行動の選択肢が増えたらここに分岐を置く。
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
