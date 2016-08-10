using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrimesCalculator
{
    class CalcPrimes
    {
        private int Last { get; set; }
        private int First { get; set; }
        private EventWaitHandle _cancelEvent;
        public List<int> _listOfNumbers;

        SynchronizationContext _syncContext;

        private Action<IEnumerable<int>> _updater;
        private Action _canceller;
        private ISynchronizeInvoke _invoker;

        public CalcPrimes(int first, int last , ISynchronizeInvoke invoker, Action<IEnumerable<int>> updater, Action canceller)
        {
            _updater = updater;
            _invoker = invoker;
            _canceller = canceller;
            First = first;
            Last = last;
            _syncContext = SynchronizationContext.Current;
        }

        public void Calculat()
        {
            _cancelEvent = new EventWaitHandle(false, EventResetMode.AutoReset, "_CancelPrimeCalc");

            _listOfNumbers = new List<int>();

            foreach (var number in Enumerable.Range(First, Last))
            {
                if (_cancelEvent.WaitOne(0))
                {
                    _cancelEvent.Close();
                    _syncContext.Send(delegate {
                        _canceller();
                    }, null);
                    return;
                }
                if (IsPrime(number))
                    _listOfNumbers.Add(number);
            }
            _syncContext.Send(delegate {
                _updater(_listOfNumbers);
            }, null);
        }

        private bool IsPrime(int num)
        {
            if ((num & 1) == 0)
            {
                if (num == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            for (int i = 3; (i * i) <= num; i += 2)
            {
                if ((num % i) == 0)
                {
                    return false;
                }
            }
            return num != 1;
        }
    }
}
