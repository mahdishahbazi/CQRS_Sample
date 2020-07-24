using SampleMR.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SampleMR.Services
{
    public class FakeCartService
    {
        private  List<CartItem> items = new List<CartItem>();

        public IList<CartItem> Items
        {
            get
            {
                return new ReadOnlyCollection<CartItem>(items);
            }
        }

        public Guid AddItem(CartItem item)
        {
            var existingItem = items.FirstOrDefault(i => i.Item.Equals(item.Item, StringComparison.InvariantCultureIgnoreCase));
            if (existingItem== null)
            {
                item.Id = Guid.NewGuid();
                items.Add(item);
                return item.Id;
            }
            else
            {
                existingItem.Quantity += item.Quantity;
                return existingItem.Id;
            }
        }

        public void DeleteItem(Guid itemId)
        {
            var existingItem = items.FirstOrDefault(i => i.Id.Equals(itemId));
            if (existingItem != null)
            {
                items.Remove(existingItem);
            }
        }

        public void ClearCart()
        {
            items = new List<CartItem>();
        }

        public async Task UpdateItem(Guid itemId, int quantity)
        {
            var existingItem = items.FirstOrDefault(i => i.Id.Equals(itemId));
            if (existingItem != null)
            {
                existingItem.Quantity = quantity;
            }
            else
            {
                throw new Exception($"{itemId} does not exist");
            }
            await Task.FromResult(0);
        }
    }
}
