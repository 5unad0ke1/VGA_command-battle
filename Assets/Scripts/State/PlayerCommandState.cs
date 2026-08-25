using Assets.Scripts.Command;
using Assets.Scripts.Entity;
using UnityEngine.UI;

namespace Assets.Scripts.State
{
    /// <summary>
    /// プレイヤーのコマンド選択待ち。ボタン入力を <see cref="IBattleCommand"/> に変換して
    /// 解決フェーズへ渡す。「どうぐ」だけはサブ状態へ遷移する。
    /// </summary>
    public sealed class PlayerCommandState : IState
    {
        public PlayerCommandState(
            IStateController controller,
            PlayerEntity player,
            EnemyEntity target,
            Button attackButton,
            Button skillButton,
            Button itemButton,
            ItemSelectState itemSelectState,
            ActionResolveState actionResolveState,
            EnemyActionState enemyActionState)
        {
            _controller = controller;
            _player = player;
            _target = target;
            _attackButton = attackButton;
            _skillButton = skillButton;
            _itemButton = itemButton;
            _itemSelectState = itemSelectState;
            _actionResolveState = actionResolveState;
            _enemyActionState = enemyActionState;

            // 購読は生成時に 1 度だけ。選択の受付可否は Init()/Exit() の interactable で切り替える。
            attackButton.onClick.AddListener(() => _selectedCommand = new AttackCommand(_player, _target));
            skillButton.onClick.AddListener(() => _selectedCommand = new SkillCommand(_player, _target));
            itemButton.onClick.AddListener(() => _wantsItem = true);
        }

        public void Init()
        {
            // 前回の選択が残っていると即座に確定してしまうため、入場のたびに初期化する。
            _selectedCommand = null;
            _wantsItem = false;
            SetButtonsInteractable(true);
        }

        public void Update()
        {
            if (_wantsItem)
            {
                _itemSelectState.Begin(_player);
                _controller.ChangeState(_itemSelectState);
                return;
            }

            if (_selectedCommand is null)
                return;

            // 注意: SP 切れでもスキルは選べる。実行時に不発になるだけでターンは消費される。
            _actionResolveState.Begin(_selectedCommand, _enemyActionState);
            _controller.ChangeState(_actionResolveState);
        }

        public void Exit()
        {
            SetButtonsInteractable(false);
        }

        private void SetButtonsInteractable(bool value)
        {
            _attackButton.interactable = value;
            _skillButton.interactable = value;
            _itemButton.interactable = value;
        }

        private readonly IStateController _controller;
        private readonly PlayerEntity _player;
        private readonly EnemyEntity _target;
        private readonly Button _attackButton;
        private readonly Button _skillButton;
        private readonly Button _itemButton;
        private readonly ItemSelectState _itemSelectState;
        private readonly ActionResolveState _actionResolveState;
        private readonly EnemyActionState _enemyActionState;

        private IBattleCommand _selectedCommand;
        private bool _wantsItem;
    }
}
