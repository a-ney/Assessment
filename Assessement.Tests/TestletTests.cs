using System.Collections.Generic;
using Xunit;
using Assessment;
using FluentAssertions;
using System.Linq;

namespace Assessement.Tests
{
    public class TestletTests
    {
        private IEnumerable<Item> GivenItems(int count, ItemTypeEnum type, int startId = 1)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new Item
                {
                    ItemId = (i + startId).ToString(),
                    ItemType = type
                };
            }
        }

        [Fact]
        public void Given4PretesetAnd6OperationalItems_WhenRandomized_ShouldReturnRandomizedResultStartingWith2PretestItems()
        {
            //assign
            var pretestItems = GivenItems(4, ItemTypeEnum.Pretest).ToList();
            var opItems = GivenItems(6, ItemTypeEnum.Operational, 5).ToList();
            var sourceItems = pretestItems.Concat(opItems).ToList();
            var testlet = new Testlet("id", sourceItems);
                
            //act
            var result = testlet.Randomize();

            //assert
            result.Should().BeEquivalentTo(sourceItems).And.NotContainInOrder(sourceItems);

            result.Take(2).Should().BeSubsetOf(pretestItems);

            result.Take(2).Should().BeSubsetOf(pretestItems);
            result.Skip(2).Should().BeEquivalentTo(pretestItems.Except(result.Take(2)).Concat(opItems));
        }
    }
}
