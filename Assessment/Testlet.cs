using System.Linq;
using System.Collections.Generic;
using System;

namespace Assessment
{
    public class Testlet
    {

        public string TestletId;

        private List<Item> _items;
        private const int ForwardingPretestItemsCount = 2;

        public Testlet(string testletId, List<Item> items)
        {
            TestletId = testletId;
            _items = items;
        }

        public List<Item> Randomize()
        {
            var rand = new Random();
            var randomized = _items.OrderBy(i => rand.Next()).ToList();

            for (int i = 0; i < ForwardingPretestItemsCount; i++)
            {
                var pretestIndex = randomized.FindIndex(i, r => r.ItemType == ItemTypeEnum.Pretest);
                if (pretestIndex != i)
                {
                    (randomized[pretestIndex], randomized[i]) = (randomized[i], randomized[pretestIndex]);
                }
            }         

            return randomized;
        }
    }

    public class Item
    {
        public string ItemId;
        public ItemTypeEnum ItemType;
    }

    public enum ItemTypeEnum
    {
        Pretest = 0,
        Operational = 1
    }
}
