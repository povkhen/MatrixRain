using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace v1
{
    class Column
    {
        public Queue<Chain> Chains { get; set; } = new Queue<Chain>();
        public bool isUpdated { get; set; } = false;

        private void AddChain(Chain chain)
        {
            Chains.Enqueue(chain);
        }

        private void RemoveChain(Chain chain)
        {
            if (Chains.Any(x => x.CurrentLocationHead == chain.CurrentLocationHead))
            {
                Chains.Dequeue();
            }
        }
        
       /// <summary>
       ///  Condition of adding new chain in queue
       /// </summary>
       /// <returns>Return true if we can/should add new chain </returns>
        private bool CheckToAdd()
        {
            if (!Chains.Any()) AddChain(new Chain());
            var lastChain = Chains.Last();
            return (lastChain.CurrentLocationHead - lastChain.WholeLenghtOfChain > 0);
        }



        /// <summary>
        ///  Condition of removing first chain in queue
        /// </summary>
        /// <returns>Return true if we should remove first chain </returns>
        private bool CheckToRemove(Chain firstChain)
        {
            return (firstChain.CurrentLocationHead - firstChain.LengthOfChain > HelperConst.WINDOWHEIGHT);
        }

        /// <summary>
        /// Update whole column
        /// </summary>
        /// <param name="speed"></param>
        public void Update(object speed)
        {
            while (true)
            {
                Thread.Sleep((int)speed);
                foreach (var chain in Chains)
                {
                    chain.CurrentLocationHead++;
                }
                if (CheckToAdd()) AddChain(new Chain());

                var firstChain = Chains.Peek();
                if (CheckToRemove(firstChain)) RemoveChain(firstChain);

                isUpdated = true;
            }
        }
    }
}
