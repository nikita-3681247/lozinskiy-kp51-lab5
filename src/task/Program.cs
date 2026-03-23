namespace task;

using task.Models;
using task.Logic;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Program application = new Program(); 
        application.Run(); 
    }

    public void Run()
    {
        Sorter inventorySorter = new Sorter();
        bool isExitRequested = false;

        while (!isExitRequested)
        {
            Console.WriteLine("===== МЕНЮ =====");
            Console.WriteLine("1. Вивести поточну колекцію");
            Console.WriteLine("2. Додати товар вручну");
            Console.WriteLine("3. Видалити товар за SKU");
            Console.WriteLine("4. Заповнити колекцію контрольними даними");
            Console.WriteLine("5. Виконати сортування");
            Console.WriteLine("6. Перемикач виведення проміжних етапів");
            Console.WriteLine("7. Вивести статистику сортування");
            Console.WriteLine("8. Вивести 10 найдешевших товарів");
            Console.WriteLine("9. Відібрати товари дешевші за вказану суму");
            Console.WriteLine("0. Вийти");
            Console.Write("Оберіть опцію: ");

            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1": 
                    inventorySorter.DisplayInventory(); 
                    break;
                
                case "2": 
                    HandleAddRecordMenu(inventorySorter); 
                    break;
                
                case "3":
                    Console.Write("Введіть SKU для видалення: ");
                    string skuToRemove = Console.ReadLine();
                    inventorySorter.RemoveItemBySku(skuToRemove);
                    break;
                
                case "4": 
                    inventorySorter.LoadTestInventory(); 
                    break;
                
                case "5": 
                    inventorySorter.ExecuteQuickSort(); 
                    break;
                
                case "6": 
                    inventorySorter.ToggleStepOutput(); 
                    break;
                
                case "7": 
                    inventorySorter.DisplayAlgorithmStats();
                    break;
                
                case "8": 
                    inventorySorter.ShowTop10CheapestItems(); 
                    break;
                
                case "9":
                    Console.Write("Введіть максимальну суму: ");
                    if (int.TryParse(Console.ReadLine(), out int maxPrice) && maxPrice > 0)
                    {
                        inventorySorter.ShowItemsCheaperThan(maxPrice);
                    }
                    else
                    {
                        Console.WriteLine("Помилка, введіть коректне додатне число.");
                    }
                    break;
                
                case "0": 
                    isExitRequested = true; 
                    break;
                
                default: 
                    Console.WriteLine("Невідома команда. Спробуйте ще раз."); 
                    break;
            }
        }
    }

    private void HandleAddRecordMenu(Sorter inventorySorter)
    {
        Console.WriteLine("\n--- Додавання нового товару ---");
        
        Console.Write("Введіть SKU: ");
        string sku = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(sku))
        {
            Console.Write("SKU не може бути порожнім. Введіть SKU: ");
            sku = Console.ReadLine();
        }

        Console.Write("Введіть назву товару: ");
        string name = Console.ReadLine();

        Console.Write("Введіть категорію: ");
        string category = Console.ReadLine();

        int price;
        Console.Write("Введіть ціну: ");
        while (!int.TryParse(Console.ReadLine(), out price) || price < 0)
        {
            Console.Write("Помилка, ціна має бути коректним додатним числом. Введіть ціну: ");
        }

        inventorySorter.AddNewItem(new Record(sku, name, category, price));
        Console.WriteLine("Товар успішно додано");
    }
}