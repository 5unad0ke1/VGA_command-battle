using UnityEngine;

namespace Assets.Scripts.State
{
    /// <summary>勝敗が確定した後の終端 State。ここからはどこへも遷移しない。</summary>
    internal sealed class BattleEndState : IState
    {
        /// <summary>勝敗を設定する。この State へ遷移する直前に必ず呼ぶこと。</summary>
        /// <param name="playerLost">プレイヤーが敗北したなら true。</param>
        public void Setup(bool playerLost)
        {
            _playerLost = playerLost;
        }

        public void Init()
        {
            // TODO: 結果表示は暫定でログ出力。決着画面の UI に差し替える。
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
