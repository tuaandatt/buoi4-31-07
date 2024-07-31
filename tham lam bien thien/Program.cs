using System;
using System.Collections.Generic;

class Item
{
    public int Value { get; set; }
    public int Weight { get; set; }

    public Item(int value, int weight)
    {
        Value = value;
        Weight = weight;
    }

    public double Ratio()
    {
        return (double)Value / Weight;
    }
}

class SelectedItem
{
    public Item Item { get; set; }
    public int Quantity { get; set; }

    public SelectedItem(Item item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }
}

class Program
{
    static Tuple<double, List<SelectedItem>> GreedyKnapsack(int capacity, List<Item> items)
    {
        items.Sort((a, b) => b.Ratio().CompareTo(a.Ratio()));

        double totalValue = 0;
        int currentWeight = 0;
        List<SelectedItem> selectedItems = new List<SelectedItem>();

        foreach (var item in items)
        {
            if (currentWeight + item.Weight <= capacity)
            {
                currentWeight += item.Weight;
                totalValue += item.Value;
                selectedItems.Add(new SelectedItem(item, 1));
            }
            else
            {
                int remainingWeight = capacity - currentWeight;
                double fraction = (double)remainingWeight / item.Weight;
                totalValue += item.Value * fraction;
                selectedItems.Add(new SelectedItem(new Item((int)(item.Value * fraction), remainingWeight), 1));
                break;
            }
        }

        return Tuple.Create(totalValue, selectedItems);
    }

    static void Main()
    {
        List<Item> items = new List<Item>
        {
            new Item(60, 10),
            new Item(120, 20),
            new Item(180, 30)
        };

        Console.Write("Nhập dung lượng tối đa của Ba lô: ");
        int capacity = int.Parse(Console.ReadLine());
        var result = GreedyKnapsack(capacity, items);
        double maxValue = result.Item1;
        List<SelectedItem> selectedItems = result.Item2;

        Console.WriteLine("Giá trị lớn nhất trong Ba lô = " + maxValue);
        Console.WriteLine("Các vật phẩm được chọn:");
        foreach (var selectedItem in selectedItems)
        {
            Console.WriteLine(" Giá trị: " + selectedItem.Item.Value + " Trọng lượng: " + selectedItem.Item.Weight + " Số lượng: " + selectedItem.Quantity);
        }
    }
}
