using System;
using Newtonsoft.Json.Linq;
using Raven.Database;
using Raven.Storage.Managed;
using Xunit;
using System.Linq;

namespace Raven.Storage.Tests
{
	public class General : TxStorageTest
	{
		[Fact]
		public void CanGetNewIdentityValue()
		{
			using (var tx = NewTransactionalStorage())
			{
				tx.Batch(mutator =>
				{
					Assert.Equal(1, mutator.General.GetNextIdentityValue("ayende"));
					Assert.Equal(1, mutator.General.GetNextIdentityValue("rahien"));

					Assert.Equal(2, mutator.General.GetNextIdentityValue("ayende"));
					Assert.Equal(2, mutator.General.GetNextIdentityValue("rahien"));	
				});


				tx.Batch(mutator =>
				{
					Assert.Equal(3, mutator.General.GetNextIdentityValue("ayende"));
					Assert.Equal(3, mutator.General.GetNextIdentityValue("rahien"));

					Assert.Equal(4, mutator.General.GetNextIdentityValue("ayende"));
					Assert.Equal(4, mutator.General.GetNextIdentityValue("rahien"));
				});
			}
		}

        [Fact]
        public void CanGetNewIdentityValueAfterRestart()
        {
            using (var tx = NewTransactionalStorage())
            {
                tx.Batch(mutator =>
                {
                    Assert.Equal(1, mutator.General.GetNextIdentityValue("ayende"));
                    Assert.Equal(1, mutator.General.GetNextIdentityValue("rahien"));

                    Assert.Equal(2, mutator.General.GetNextIdentityValue("ayende"));
                    Assert.Equal(2, mutator.General.GetNextIdentityValue("rahien"));
                });
            }

            using(var tx = NewTransactionalStorage())
            {

                tx.Batch(mutator =>
                {
                    Assert.Equal(3, mutator.General.GetNextIdentityValue("ayende"));
                    Assert.Equal(3, mutator.General.GetNextIdentityValue("rahien"));

                    Assert.Equal(4, mutator.General.GetNextIdentityValue("ayende"));
                    Assert.Equal(4, mutator.General.GetNextIdentityValue("rahien"));
                });
            }
        }

	}
}