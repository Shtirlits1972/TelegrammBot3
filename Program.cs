using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using System.IO;
using System.Text;
using System.Configuration;

namespace TelegrammBot3
{
    class Program
    {
        public static TelegramBotClient bot = new TelegramBotClient(ConfigurationManager.AppSettings["Token"]);
        private static List<Order> listOrder = new List<Order>();
        private static int LastMessageId = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Start Bot!");

            bot.OnMessage += Bot_OnMessageAsync;
            bot.StartReceiving();
            bot.OnCallbackQuery += Bot_OnCallbackQuery;

            Console.ReadLine();
        }

        private static void Bot_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            string data = e.CallbackQuery.Data;
            Console.WriteLine(data);
            long chatId = e.CallbackQuery.Message.Chat.Id;
            Order order = GetOrderByChatId(chatId);

            if (data == "end")
            {
                RemoveOrderByChatId(chatId);

                bot.SendTextMessageAsync(
                   chatId: chatId,
                   text: $" Для нового заказа наберите команду /start \n");
            }
            else if (data == "endgoods")
            {
                ChangetOrderStatus(chatId, OrderState.goods);
                //  город
                InlineKeyboardMarkup inlineKeyboard = Ut.GetCityesBtns();

                    bot.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Выберете город из списка: \n",
                    replyMarkup: inlineKeyboard  );
            }
            else if((data != "end") && (data != "endgoods"))
            {
                if (order.state == OrderState.start)
                {
                    if (InProductList(data))
                    {
                        AddProduct(chatId, data);

                        InlineKeyboardMarkup inlineKeyboard = Ut.GetProductBtns();

                        if(LastMessageId != 0)
                        {
                            bot.DeleteMessageAsync(chatId, LastMessageId);
                        }

                        bot.SendTextMessageAsync(
                           chatId: chatId,
                           text: $" Ваш заказ:\n {order} \r\n Продолжайте выбирать товар: \n",
                           replyMarkup: inlineKeyboard   );
                        return;
                    }
                }
                else if(order.state == OrderState.goods)
                {
                    order.City = data;
                    ChangetOrderStatus(chatId, OrderState.city);

                    bot.SendTextMessageAsync(
                    chatId: chatId,
                    text: $" Ваш заказ:\n {order.ToString()} \r\n введите адрес доставки: ");
                    return;
                }
            }

            LastMessageId = e.CallbackQuery.Message.MessageId;
        }

        private static void Bot_OnMessageAsync(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            TelegramBotClient bot = (TelegramBotClient)sender;
            Console.WriteLine(e.Message.Text);
            long chatId = e.Message.Chat.Id;

            if (e.Message.Text == "/start")
            {
                Order order = new Order { chatId = e.Message.Chat.Id, state = OrderState.start };
                listOrder.Add(order);

                InlineKeyboardMarkup inlineKeyboard = Ut.GetProductBtns();

                bot.SendTextMessageAsync(
                   chatId: chatId,
                   text: "Добро пожаловать!\n Выбирай товар: \n",
                   replyMarkup: inlineKeyboard
                   );
            }
            else
            {
                Order order = GetOrderByChatId(chatId);

                if(order.state == OrderState.city)
                {
                    order.Adress = e.Message.Text;
                    string path = $"{chatId}.txt";

                    try
                    {
                        using (TextWriter wr = File.AppendText(path))
                        {
                            DateTime dt = DateTime.Now;
                            string order_header = $"Заказ № {chatId} от {dt.ToString("g")} \r\n";
                            wr.Write(order_header + order.ToString(), Encoding.UTF8);
                        }
                    }
                    catch (Exception ex)
                    {
                        string Error = ex.Message;
                    }

                    ChangetOrderStatus(chatId, OrderState.end);
                    Console.WriteLine(order);

                    bot.SendTextMessageAsync(
                       chatId: chatId,
                       text: $" Ваш заказ: № {chatId} \n {order} \r\n Спасибо за покупку! ");

                }
            }

        }

        private static void AddProduct(long chatId, string productName)
        {
            Order order = GetOrderByChatId(chatId);

            for (int i = 0; i < order.products.Count; i++)
            {
                if (order.products[i].product.NameProduct == productName)
                {
                    order.products[i].qty++;
                    return;
                }
            }

            Product product = GetProductByName(productName);
            OrderLine orderLine = new OrderLine { product = product, qty = 1 };

            order.products.Add(orderLine);
        }

        private static Product GetProductByName(string NameProduct)
        {
            List<Product> products = Ut.GetProducts();

            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].NameProduct == NameProduct)
                {
                    return products[i];
                }
            }
            return null;
        }

        private static bool InCityList(string CityName)
        {
            List<string> cityes = Ut.GetCityes();
            for (int i = 0; i < cityes.Count; i++)
            {
                if (cityes[i] == CityName)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool InProductList(string ProductName)
        {
            bool flag = false;
            List<Product> products = Ut.GetProducts();

            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].NameProduct == ProductName)
                {
                    return true;
                }
            }
            return flag;
        }
        private static void RemoveOrderByChatId(long chatId)
        {
            int index = 0;
            bool flag = false;
            for (int i = 0; i < listOrder.Count; i++)
            {
                if (listOrder[i].chatId == chatId)
                {
                    flag = true;
                    index = i;
                    break;
                }
            }

            if (flag)
            {
                listOrder.RemoveAt(index);
            }
        }
        private static Order GetOrderByChatId(long chatId)
        {
            Order order = new Order { state = OrderState.start };
            for (int i = 0; i < listOrder.Count; i++)
            {
                if (listOrder[i].chatId == chatId)
                {
                    return listOrder[i];
                }
            }
            return order;
        }
        private static void ChangetOrderStatus(long chatId, OrderState state)
        {
            for (int i = 0; i < listOrder.Count; i++)
            {
                if (listOrder[i].chatId == chatId)
                {
                    listOrder[i].state = state;
                }
            }
        }
    }
}
