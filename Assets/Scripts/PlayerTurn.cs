using Assets.Scripts.Entity;
using UnityEngine.UI;

namespace Assets.Scripts
{
    internal sealed class PlayerTurn : IState
    {
        public PlayerTurn(
            Button attackCommandButton,
            Button skillCommandButton,
            Button itemCommandButton
            )
        {
            attackCommandButton.onClick.AddListener(() => _isClickedAttackCmd = true);
            skillCommandButton.onClick.AddListener(() => _isClickedSkillCmd = true);
            itemCommandButton.onClick.AddListener(() => _isClickedItemCmd = true);
        }
        public void Init()
        {
            _isClickedAttackCmd = false;
            _isClickedSkillCmd = false;
            _isClickedItemCmd = false;
        }
        public void Update()
        {
            if (!_isClickedAttackCmd && !_isClickedItemCmd && !_isClickedSkillCmd)
                return;

            if (_isClickedAttackCmd)
            {
                _player.Attack(_target);
            }

            if (_isClickedSkillCmd)
            {
                _player.SkillAttack(_target);
            }

            if (_isClickedItemCmd)
            {
                _isClickedItemCmd = false;
            }

            _stateController.ExitCallout(this);
        }

        public void Exit()
        {
        }

        private readonly IStateController _stateController;
        private readonly PlayerEntity _player;
        private readonly IDamageable _target;

        private bool _isClickedAttackCmd;
        private bool _isClickedSkillCmd;
        private bool _isClickedItemCmd;
    }
}
