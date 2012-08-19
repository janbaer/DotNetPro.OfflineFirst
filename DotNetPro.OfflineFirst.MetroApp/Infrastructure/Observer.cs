using System;

namespace DotNetPro.OfflineFirst.MetroApp.Infrastructure
{
    public class Observer<T> : IObserver<T>
    {
        private readonly Action _onCompleteAction;
        private readonly Action<Exception> _onErrorAction;
        private readonly Action<T> _onNextAction;

        public Observer(Action onCompleteAction, Action<Exception> onErrorAction, Action<T> onNextAction)
        {
            _onCompleteAction = onCompleteAction;
            _onErrorAction = onErrorAction;
            _onNextAction = onNextAction;
        }

        public void OnCompleted()
        {
            if (_onCompleteAction != null)
            {
                _onCompleteAction.Invoke();
            }
        }

        public void OnError(Exception error)
        {
            if (_onErrorAction != null)
            {
                _onErrorAction.Invoke(error);
            }
        }

        public void OnNext(T value)
        {
            if (_onNextAction != null)
            {
                _onNextAction.Invoke(value);
            }
        }
    }
}