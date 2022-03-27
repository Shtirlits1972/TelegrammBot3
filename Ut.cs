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
            InlineKeyboardMarkup inlineKeyboard = new(
                 new[] {
                            new [] { InlineKeyboardButton.WithCallbackData("Водка 1 бут.", "Водка") },
                            new [] { InlineKeyboardButton.WithCallbackData("Носки - 1 пара", "Носки") },
                            new [] { InlineKeyboardButton.WithCallbackData("Пиво - 1 бут", "Пиво") },
                            new [] { InlineKeyboardButton.WithCallbackData("Сигареты - 1 уп.", "Сигареты") },

                            new [] { InlineKeyboardButton.WithCallbackData("Завершить заказ", "end"),
                                       InlineKeyboardButton.WithCallbackData("Завершить выбор товара", "endgoods") }
                      }
                  );

            return inlineKeyboard;
        }


        public static InlineKeyboardMarkup GetCityesBtns()
        {
            InlineKeyboardMarkup inlineKeyboard = new(
                 new[] {
                            new [] { InlineKeyboardButton.WithCallbackData("Харьков") },
                            new [] { InlineKeyboardButton.WithCallbackData("Одесса") },
                            new [] { InlineKeyboardButton.WithCallbackData("Днепропетровск") },
                            new [] { InlineKeyboardButton.WithCallbackData("Полтава") },
                            new [] { InlineKeyboardButton.WithCallbackData("Сумы") },

                            new [] { InlineKeyboardButton.WithCallbackData("Завершить заказ", "end") }
                      }
                  );

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
