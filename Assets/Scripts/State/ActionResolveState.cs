using Assets.Scripts.Command;
using Assets.Scripts.Entity;

namespace Assets.Scripts.State
{
    /// <summary>
    /// 仕様書(PDF)の「行動選択の結果」フェーズ。<see cref="Begin"/> で渡されたコマンドを
    /// 1 つ実行し、勝敗判定のうえで指定された次状態(敵ターン or プレイヤーターン)へ遷移する。
    /// インスタンスは使い回されるため、遷移のたびに <see cref="Begin"/> が要る。
    /// </summary>
    internal sealed class ActionResolveState : IState
    {
        public ActionResolveState(IStateController controller, PlayerEntity player, EnemyEntity enemy, BattleEndState battleEndState)
        {
            _controller = controller;
            _player = player;
            _enemy = enemy;
            _battleEndState = battleEndState;
        }

        /// <summary>
        /// 実行するコマンドと、解決後に遷移する State を設定する。
        /// この State へ遷移する直前に必ず呼ぶこと。
        /// </summary>
        public void Begin(IBattleCommand command, IState nextState)
        {
            _command = command;
            _nextState = nextState;
        }

        public void Init()
        {
            // コマンドの実行は入場時の 1 回だけ。持ち越さないよう参照はここで手放す。
            _command.Execute();
            _command = null;
        }

        public void Update()
        {
            // 遷移を Update() で行うことで、実行結果が表示に反映されてから次状態へ進む。
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
