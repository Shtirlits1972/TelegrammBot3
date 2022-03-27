using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegrammBot3
{
    public class Ut
    {
        public static InlineKeyboardMarkup GetProductBtns()
        {
            List<Product> products = GetProducts();
            List<InlineKeyboardButton[]> lst = new List<InlineKeyboardButton[]>();

            for (int i = 0; i < products.Count; i++)
            {
                InlineKeyboardButton[] arr1 = new[] { InlineKeyboardButton.WithCallbackData($"{products[i].NameProduct} - 1 {products[i].Unit}", products[i].NameProduct) };
                lst.Add(arr1);
            }

            InlineKeyboardButton[] closeBtn = new[] { InlineKeyboardButton.WithCallbackData("Завершить заказ", "end"),
                                                      InlineKeyboardButton.WithCallbackData("Завершить выбор товара", "endgoods") };
            lst.Add(closeBtn);

            InlineKeyboardMarkup inlineKeyboard = new(lst);

            return inlineKeyboard;
        }
        public static InlineKeyboardMarkup GetCityesBtns()
        {
            List<string> cityes = GetCityes();
            List<InlineKeyboardButton[]> lst = new List<InlineKeyboardButton[]>();

            for (int i = 0; i < cityes.Count; i++)
            {
                InlineKeyboardButton[] arr1 = new[] { InlineKeyboardButton.WithCallbackData(cityes[i]) };
                lst.Add(arr1);
            }

            InlineKeyboardButton[] closeBtn = new[] { InlineKeyboardButton.WithCallbackData("Завершить заказ", "end") };
            lst.Add(closeBtn);

            InlineKeyboardMarkup inlineKeyboard = new(lst);
            return inlineKeyboard;
        }

        public static List<string> GetCityes()
        {
            List<string> cityes = new List<string>();
            cityes.Add("Харьков");
            cityes.Add("Одесса");
            cityes.Add("Днепропетровск");
            cityes.Add("Полтава");
            cityes.Add("Сумы");

            return cityes;
        }

        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            Product socs = new Product { NameProduct = "Носки", price = 10, Unit = "пара" };
            products.Add(socs);
            Product vodka = new Product { NameProduct = "Водка", price = 100, Unit = "бутылка 0,5" };
            products.Add(vodka);
            Product pivo = new Product { NameProduct = "Пиво", price = 20, Unit = "бутылка 0,5" };
            products.Add(pivo);
            Product cigarette = new Product { NameProduct = "Сигареты", price = 30, Unit = "пачка 20 сигарет" };
            products.Add(cigarette);

            return products;
        }
    }
}
