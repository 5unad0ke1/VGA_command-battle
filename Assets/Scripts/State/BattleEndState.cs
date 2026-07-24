using UnityEngine;

namespace Assets.Scripts.State
{
    internal sealed class BattleEndState : IState
    {
        public void Setup(bool playerLost)
        {
            _playerLost = playerLost;
        }

        public void Init()
        {
            Debug.Log(_playerLost ? "はいぼく……" : "しょうり！");
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }

        private bool _playerLost;
    }
}
