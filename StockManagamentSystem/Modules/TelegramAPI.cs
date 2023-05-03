using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using System.Net.NetworkInformation;
using Telegram.Bot.Types;


namespace StockManagamentSystem.Modules
{
    public class TelegramAPI
    {
        //6122998426 - -1001954121439
        static TelegramBotClient botClient = new TelegramBotClient("5905663544:AAHvhQz3NB08wKiGa0zGsXMwrPnhCxh0QTc");
        const long CHAT_ID = -1001954121439;

        public async static void SendMessage(string msg)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                await botClient.SendTextMessageAsync(CHAT_ID, msg);
            }
        }

    }
}
