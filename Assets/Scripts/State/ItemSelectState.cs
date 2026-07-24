using Assets.Scripts.Command;
using Assets.Scripts.Entity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.State
{
    // 「どうぐ」コマンド選択時のサブ状態。「やめる」でコマンド選択に戻る。
    internal sealed class ItemSelectState : IState
    {
        public ItemSelectState(
            GameObject parent,
            IStateController controller,
            IReadOnlyList<Button> itemButtons,
            Button cancelButton,
            ActionResolveState actionResolveState)
        {
            _parent = parent;
            _controller = controller;
            _itemButtons = itemButtons;
            _actionResolveState = actionResolveState;

            for (var i = 0; i < itemButtons.Count; i++)
            {
                var index = i;
                itemButtons[i].onClick.AddListener(() => _chosenIndex = index);
            }

            cancelButton.onClick.AddListener(() => _cancelled = true);
        }

        // PlayerCommandStateと相互参照になるため、生成後に配線する。
        public void Configure(PlayerCommandState playerCommandState, EnemyActionState enemyActionState)
        {
            _playerCommandState = playerCommandState;
            _enemyActionState = enemyActionState;
        }

        public void Begin(PlayerEntity player)
        {
            _player = player;
        }

        public void Init()
        {
            _parent.SetActive(true);
            _chosenIndex = -1;
            _cancelled = false;
            RefreshButtons();
        }

        public void Update()
        {
            if (_cancelled)
            {
                _controller.ChangeState(_playerCommandState);
                return;
            }

            if (_chosenIndex < 0)
                return;

            var command = new ItemCommand(_player, _chosenIndex, _player);
            _actionResolveState.Begin(command, _enemyActionState);
            _controller.ChangeState(_actionResolveState);
        }

        public void Exit()
        {
            _parent.SetActive(false);
        }

        private void RefreshButtons()
        {
            var items = _player.Inventory.Items;
            for (var i = 0; i < _itemButtons.Count; i++)
                _itemButtons[i].gameObject.SetActive(i < items.Count);
        }

        private readonly IStateController _controller;
        private readonly IReadOnlyList<Button> _itemButtons;
        private readonly ActionResolveState _actionResolveState;
        private readonly GameObject _parent;

        private PlayerCommandState _playerCommandState;
        private EnemyActionState _enemyActionState;
        private PlayerEntity _player;

        private int _chosenIndex;
        private bool _cancelled;
    }
}
