using Assets.Scripts.Command;
using Assets.Scripts.Entity;
using UnityEngine.UI;

namespace Assets.Scripts.State
{
    internal sealed class PlayerCommandState : IState
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

            attackButton.onClick.AddListener(() => _selectedCommand = new AttackCommand(_player, _target));
            skillButton.onClick.AddListener(() => _selectedCommand = new SkillCommand(_player, _target));
            itemButton.onClick.AddListener(() => _wantsItem = true);
        }

        public void Init()
        {
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
