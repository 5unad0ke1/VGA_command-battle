using Assets.Scripts.Command;
using Assets.Scripts.Entity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.State
{
    /// <summary>
    /// 「どうぐ」コマンド選択時のサブ状態。アイテムを選ぶと解決フェーズへ進み、
    /// 「やめる」でコマンド選択へ戻る。
    /// </summary>
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

            // 購読は生成時に 1 度だけ。クリックはフィールドに記録するに留め、
            // 実際の遷移は Update() で行う(コールバック内から状態を変えない)。
            for (var i = 0; i < itemButtons.Count; i++)
            {
                var index = i;
                itemButtons[i].onClick.AddListener(() => _chosenIndex = index);
            }

            cancelButton.onClick.AddListener(() => _cancelled = true);
        }

        /// <summary>
        /// 遷移先を配線する。<see cref="PlayerCommandState"/> と相互参照になり
        /// コンストラクタでは解決できないため、生成後に呼ぶ。
        /// </summary>
        public void Configure(PlayerCommandState playerCommandState, EnemyActionState enemyActionState)
        {
            _playerCommandState = playerCommandState;
            _enemyActionState = enemyActionState;
        }

        /// <summary>どうぐを使う対象を設定する。この State へ遷移する直前に必ず呼ぶこと。</summary>
        public void Begin(PlayerEntity player)
        {
            _player = player;
        }

        public void Init()
        {
            _parent.SetActive(true);

            // 前回の選択が残っていると即座に確定してしまうため、入場のたびに初期化する。
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

            // どうぐの使用者と対象は現状どちらもプレイヤー自身。
            var command = new ItemCommand(_player, _chosenIndex, _player);
            _actionResolveState.Begin(command, _enemyActionState);
            _controller.ChangeState(_actionResolveState);
        }

        public void Exit()
        {
            _parent.SetActive(false);
        }

        // TODO: ItemData.Name をボタンのラベルに反映する。現状は所持数に応じた表示/非表示のみ。
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
