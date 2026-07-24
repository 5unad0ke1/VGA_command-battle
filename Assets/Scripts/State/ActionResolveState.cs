using Assets.Scripts.Command;
using Assets.Scripts.Entity;

namespace Assets.Scripts.State
{
    // PDFの「行動選択の結果」フェーズ。渡されたコマンドを1つ実行し、
    // 勝敗判定のうえで指定された次状態(敵ターン or プレイヤーターン)へ遷移する。
    internal sealed class ActionResolveState : IState
    {
        public ActionResolveState(IStateController controller, PlayerEntity player, EnemyEntity enemy, BattleEndState battleEndState)
        {
            _controller = controller;
            _player = player;
            _enemy = enemy;
            _battleEndState = battleEndState;
        }

        public void Begin(IBattleCommand command, IState nextState)
        {
            _command = command;
            _nextState = nextState;
        }

        public void Init()
        {
            _command.Execute();
            _command = null;
        }

        public void Update()
        {
            if (_player.IsDead || _enemy.IsDead)
            {
                _battleEndState.Setup(_player.IsDead);
                _controller.ChangeState(_battleEndState);
                return;
            }

            _controller.ChangeState(_nextState);
        }

        public void Exit()
        {
        }

        private readonly IStateController _controller;
        private readonly PlayerEntity _player;
        private readonly EnemyEntity _enemy;
        private readonly BattleEndState _battleEndState;

        private IBattleCommand _command;
        private IState _nextState;
    }
}
