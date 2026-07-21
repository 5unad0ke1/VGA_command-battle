using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public sealed class InGameManager : MonoBehaviour, IStateController
    {
        void Start()
        {
            Loop(destroyCancellationToken).Forget();
        }


        private async UniTask Loop(CancellationToken token)
        {
        }

        public void ExitCallout(IState from)
        {
            if (from != _currentTurn)
                return;

            _currentTurn = from switch
            {
                PlayerTurn => _enemyTurn,
                EnemyTurn => _playerTurn,
                _ => throw new Exception()
            };
        }

        private IState _currentTurn;

        private PlayerTurn _playerTurn;
        private EnemyTurn _enemyTurn;

        [SerializeField] private Button AttackCommandButton;
        [SerializeField] private Button SkillCommandButton;
        [SerializeField] private Button ItemCommandButton;
    }
}