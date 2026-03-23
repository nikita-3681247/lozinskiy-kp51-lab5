namespace task.Logic;

using System;
using System.Collections.Generic;
using task.Models;

class Sorter
{
    private List<Record> inventory;
    public SortStatistics Stats { get; private set; }
    public bool IsStepOutputEnabled { get; set; } = true;

    public Sorter()
    {
        InitializeInventory();
        Stats = new SortStatistics();
    }

    public void InitializeInventory()
    {
        inventory = new List<Record>();
    }

    public void AddNewItem(Record record)
    {
        inventory.Add(record);
    }

    public void RemoveItemBySku(string sku)
    {
        if (string.IsNullOrEmpty(sku))
        {
            return;
        }

        bool isFound = false;
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Sku.ToLower() == sku.ToLower())
            {
                inventory.RemoveAt(i);
                Console.WriteLine("Товар видалено");
                isFound = true;
                break;
            }
        }
        if (!isFound)
        {
            Console.WriteLine("Товар з таким SKU не знайдено");
        }
    }

    public void DisplayInventory()
    {
        if (inventory.Count == 0)
        {
            Console.WriteLine("Колекція порожня");
            return;
        }
        for (int i = 0; i < inventory.Count; i++)
        {
            Console.WriteLine(inventory[i]);
        }
    }

    public void LoadTestInventory()
    {
        InitializeInventory();
        AddNewItem(new Record("SKU001", "Ноутбук", "Електроніка", 25000));
        AddNewItem(new Record("SKU002", "Мишка", "Аксесуари", 500));
        AddNewItem(new Record("SKU003", "Клавіатура", "Аксесуари", 1200));
        AddNewItem(new Record("SKU004", "Монітор", "Електроніка", 8000));
        AddNewItem(new Record("SKU005", "Кабель HDMI", "Аксесуари", 300));
        AddNewItem(new Record("SKU006", "Флешка", "Аксесуари", 300));
        AddNewItem(new Record("SKU007", "Килимок для миші", "Аксесуари", 250));
        AddNewItem(new Record("SKU008", "Смартфон", "Електроніка", 15000));
        AddNewItem(new Record("SKU009", "Чохол для телефону", "Аксесуари", 200));
        AddNewItem(new Record("SKU010", "Павербанк", "Електроніка", 1500));
        AddNewItem(new Record("SKU011", "Навушники", "Електроніка", 1500));
        AddNewItem(new Record("SKU012", "Зарядний пристрій", "Аксесуари", 400));
        Console.WriteLine("Дані згенеровано");
    }

    public void ExecuteQuickSort()
    {
        Stats.ResetMetrics();
        DateTime startTime = DateTime.Now;

        if (inventory.Count > 1)
        {
            RunQuickSortRecursive(0, inventory.Count - 1);
        }

        Stats.ExecutionTime = DateTime.Now - startTime;
        Console.WriteLine("Сортування Quick Sort завершено");
    }

    private void RunQuickSortRecursive(int lowIndex, int highIndex)
    {
        Stats.RecursiveCalls++;
        if (lowIndex < highIndex)
        {
            int pivotIndex = PartitionArray(lowIndex, highIndex);
            RunQuickSortRecursive(lowIndex, pivotIndex - 1);
            RunQuickSortRecursive(pivotIndex + 1, highIndex);
        }
    }

    private int PartitionArray(int lowIndex, int highIndex)
    {
        Record pivotElement = inventory[highIndex];
        
        if (IsStepOutputEnabled)
        {
            Console.WriteLine($"Опорний елемент: {pivotElement.ProductName} - {pivotElement.Price}");
        }

        int i = lowIndex - 1;

        for (int j = lowIndex; j < highIndex; j++)
        {
            Stats.Comparisons++;
            bool isCurrentLess = false;

            if (inventory[j].Price < pivotElement.Price)
            {
                isCurrentLess = true;
            }
            else if (inventory[j].Price == pivotElement.Price)
            {
                Stats.Comparisons++; 
                if (string.Compare(inventory[j].ProductName, pivotElement.ProductName, StringComparison.OrdinalIgnoreCase) < 0)
                {
                    isCurrentLess = true;
                }
            }

            if (isCurrentLess)
            {
                i++;
                SwapElements(i, j);
            }
        }
        SwapElements(i + 1, highIndex);
        return i + 1;
    }

    private void SwapElements(int i, int j)
    {
        if (i == j) return;
        Stats.Swaps++;
        Record temp = inventory[i];
        inventory[i] = inventory[j];
        inventory[j] = temp;
    }

    public void ToggleStepOutput()
    {
        IsStepOutputEnabled = !IsStepOutputEnabled; 
        
        if (IsStepOutputEnabled)
        {
            Console.WriteLine("Показ кроків сортування увімкнено");
        }
        else
        {
            Console.WriteLine("Показ кроків сортування вимкнено");
        }
    }

    public void DisplayAlgorithmStats()
    {
        Console.WriteLine("=== Статистика алгоритму Quick Sort ===");
        Console.WriteLine($"Кількість порівнянь: {Stats.Comparisons}");
        Console.WriteLine($"Кількість перестановок: {Stats.Swaps}");
        Console.WriteLine($"Кількість рекурсивних викликів: {Stats.RecursiveCalls}");
        Console.WriteLine($"Час виконання: {Stats.ExecutionTime.TotalMilliseconds} мс");
        Console.WriteLine("=======================================");
    }

    public void ShowTop10CheapestItems()
    {
        Console.WriteLine("--- 10 найдешевших товарів ---");
        int limit = Math.Min(10, inventory.Count);
        if (limit == 0)
        {
            Console.WriteLine("Колекція порожня");
        }
        
        for (int i = 0; i < limit; i++)
        {
            Console.WriteLine($"{i + 1}. {inventory[i]}");
        }
    }

    public void ShowItemsCheaperThan(int maxPriceThreshold)
    {
        Console.WriteLine($"--- Товари дешевші за {maxPriceThreshold} ---");
        bool isFound = false;
        
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Price < maxPriceThreshold)
            {
                Console.WriteLine(inventory[i]);
                isFound = true;
            }
            else
            {
                break; 
            }
        }

        if (!isFound)
        {
            Console.WriteLine("Таких товарів не знайдено");
        }
    }
}