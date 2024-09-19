using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace MoneyMonitor
{
    public class User
    {
        [Key] public int Id { get; set; }
        public DateTime date { get; set; }
        public string? name { get; set; }
        public string? pass { get; set; }
    
    }

    public class Filter
    {
        public DateTime dateb { get; set; }
        public DateTime datee { get; set; }
        public int category { get; set; }
        public int subcategory { get; set; }
        public double money { get; set; }
    }

    public class Finance
    {
        [Key] public int Id { get; set; }
        public DateTime date { get; set; }
        public double Money { get; set; }
        public int category { get; set; }
        public int subcategory { get; set; }
        public string? comment { get; set; }
    }
    public class FinanceRu
    {
        [Key] public int Id { get; set; }
        public DateTime date { get; set; }
        public double Money { get; set; }
        public string? category { get; set; }
        public string? subcategory { get; set; }
        public string? comment { get; set; }
    }
    public static class Categoryinfo
    {

        public static Dictionary<int, string> Food { get;} = new Dictionary<int, string>()
        {
            [0] = "",
            [1] = "Питание",
            [2] = "Фрукты",
            [3] = "Сладости"
        };
        public static Dictionary<int, string> VP { get;} = new Dictionary<int, string>()
        {
            [0] = "",
            [1] = "Сигареты",
            [2] = "Алкоголь",
        };
        public static Dictionary<int, string> House { get; } = new Dictionary<int, string>()
        {
            [0] = "",
            [1] = "Ипотека",
            [2] = "Аренда",
            [3] = "Комунальные услуги",
        };
        public static Dictionary<int, string> Transport { get;} = new Dictionary<int, string>()
        {
            [0] = "",
            [1] = "Общественный транспрт",
            [2] = "Такси",
            [3] = "Каршеринг",
        };
        public static Dictionary<int, string> fun { get;} = new Dictionary<int, string>()
        {
            [0] = "",
        };
        public static Dictionary<int, string> none { get; } = new Dictionary<int, string>()
        {
            [0] = "",
        };
        public static Dictionary<int,string> category { get; } = new Dictionary<int,string>()
        {
            [0] = "",
            [1] = "Еда",
            [2] = "Вредные привычки",
            [3] = "Жилье",
            [4] = "Транспорт",
            [5] = "Развлечения"
        };

        public static Dictionary<int, Dictionary<int, string>> subcategory { get; } = new Dictionary<int, Dictionary<int, string>>()
        {
            [0] = none,
            [1] = Food,
            [2] = VP,
            [3] = House,
            [4] = Transport,
            [5] = fun
        };
    }
}


