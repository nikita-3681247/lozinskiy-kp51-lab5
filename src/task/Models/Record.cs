namespace task.Models;

class Record
{
    public string Sku { get; set; }
    public string ProductName { get; set; }
    public string Category { get; set; }
    public int Price { get; set; }

    public Record(string sku, string productName, string category, int price)
    {
        Sku = sku;
        ProductName = productName;
        Category = category;
        Price = price;
    }

    public override string ToString()
    {
        return $"SKU: {Sku} | Назва: {ProductName} | Категорія: {Category} | Ціна: {Price}";
    }
}