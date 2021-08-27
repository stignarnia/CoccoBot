using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using System.Text.RegularExpressions;

/*
 random - spara una risposta preimpostata a caso
 1 - nice
 2 - tomè
 3 - eh ma io ho sonno
 4 - gna
 5 - sei un cucco
 6 - marcio
 7 - disastro
 8 - chungus
 9 - beesechurger
 10 - inr
 11 - cringe
 12 - based
 13 - redpillato
 14 - abbiamo un solo pianeta porca di quella troia
 15 - siamo nell'anno in corso
 16 - jibi jaba
 17 - sei troppo coperta per i miei gusti
 18 - lengua lombarda alert
 19 - relativismo alert
 20 - A-word
 21 - classe A
 22 - ahahahaha
 23 - è meglio essere una roccia che una stalattite
 24 - ma giollo dov'è?
 25 - i polli non sono vere specie, sono cibo che cammina
 26 - circle jerk
 27 - No! Questo non lo accetto!
 28 - ora basta guardare, fammi assaggiare!
 29 - è come se fossi nudo
 30 - Morshu
 31 - Bruh
 */

namespace CoccoBot
{
    class Program
    {
        public static string key = System.IO.File.ReadAllText(@"key.txt");
        static TelegramBotClient Bot = new TelegramBotClient(key);
        /* MODIFICA QUI */
        static public int risposte = 31;
        /* */

        static void Main(string[] args)
        {
            Bot.StartReceiving();
            Bot.OnMessage += Bot_Onmessage;

            Console.ReadLine();
        }

        static void Bot_Onmessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                if (e.Message.Text.StartsWith("/"))
                {
                    Regex rgx = new Regex(@"\d");
                    string senzaChiocciola = e.Message.Text.Split('@')[0];
                    if (rgx.IsMatch(e.Message.Text) == true)
                    {
                        Invia_Risposta_Preimpostata(Int32.Parse(senzaChiocciola.Split('/')[1]), e.Message.Chat.Id);
                    }
                    else if (senzaChiocciola == "/random")
                    {
                        Random rnd = new Random();
                        int number = rnd.Next(1, risposte + 1);
                        Invia_Risposta_Preimpostata(number, e.Message.Chat.Id);
                    }
                }
                if (e.Message.Text.Contains("69") || e.Message.Text.Contains("420") || e.Message.Text.Contains("96")
                    || e.Message.Text.Contains("666"))
                {
                    Invia_Risposta_Preimpostata(1, e.Message.Chat.Id);
                }
                Random rnd_2 = new Random();
                int prob = rnd_2.Next(1, 201);
                if (prob == 1)
                {
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, "Eh ma io ho sonno", replyToMessageId: e.Message.MessageId);
                }
                if (e.Message.Text.ToLower().Contains("ambient") || e.Message.Text.ToLower().Contains("greta") || e.Message.Text.ToLower().Contains("inquin")
                    || e.Message.Text.ToLower().Contains("green "))
                {
                    Invia_Risposta_Preimpostata(14, e.Message.Chat.Id);
                }
                if (e.Message.Text.ToLower().Contains("animal") || e.Message.Text.ToLower().Contains("vegan"))
                {
                    Invia_Risposta_Preimpostata(25, e.Message.Chat.Id);
                }
                if (e.Message.Text.ToLower().Contains("morshu"))
                {
                    Invia_Risposta_Preimpostata(30, e.Message.Chat.Id);
                }
            }
            if (e.Message.Type.ToString() == "Photo")
            {
                Random rnd_3 = new Random();
                int prob_3 = rnd_3.Next(1, 11);
                if (prob_3 == 1)
                {
                    Invia_Risposta_Preimpostata(17, e.Message.Chat.Id);
                }
            }
        }

        static void Invia_Risposta_Preimpostata(int risposta, long id)
        {
            switch (risposta)
            {
                /* MODIFICA QUI */
                case 1:
                    Bot.SendTextMessageAsync(id, "Nice");
                    break;
                case 2:
                    Bot.SendTextMessageAsync(id, "Tomè");
                    break;
                case 3:
                    Bot.SendTextMessageAsync(id, "Eh ma io ho sonno");
                    break;
                case 4:
                    Bot.SendTextMessageAsync(id, "Gna");
                    break;
                case 5:
                    Bot.SendTextMessageAsync(id, "Sei un cucco");
                    break;
                case 6:
                    Bot.SendTextMessageAsync(id, "Marcio");
                    break;
                case 7:
                    Bot.SendTextMessageAsync(id, "Disastro");
                    break;
                case 8:
                    Bot.SendTextMessageAsync(id, "Chungus");
                    break;
                case 9:
                    Bot.SendTextMessageAsync(id, "Beesechurger");
                    break;
                case 10:
                    Bot.SendTextMessageAsync(id, "INR");
                    break;
                case 11:
                    Bot.SendTextMessageAsync(id, "Cringe");
                    break;
                case 12:
                    Bot.SendTextMessageAsync(id, "Based");
                    break;
                case 13:
                    Bot.SendTextMessageAsync(id, "Redpillato");
                    break;
                case 14:
                    Bot.SendTextMessageAsync(id, "Abbiamo un solo pianeta porca di quella troia");
                    break;
                case 15:
                    string anno = DateTime.Now.Year.ToString();
                    Bot.SendTextMessageAsync(id, "Siamo nel " + anno + " porca di quella troia");
                    break;
                case 16:
                    Bot.SendTextMessageAsync(id, "Jibi Jaba");
                    break;
                case 17:
                    Bot.SendTextMessageAsync(id, "Sei troppo coperta per i miei gusti");
                    break;
                case 18:
                    Bot.SendTextMessageAsync(id, "Lengua Lombarda alert");
                    break;
                case 19:
                    Bot.SendTextMessageAsync(id, "Relativismo alert");
                    break;
                case 20:
                    Bot.SendTextMessageAsync(id, "A-word");
                    break;
                case 21:
                    Bot.SendTextMessageAsync(id, "Classe A");
                    break;
                case 22:
                    Bot.SendTextMessageAsync(id, "ahahahaha");
                    break;
                case 23:
                    Bot.SendTextMessageAsync(id, "È meglio essere una roccia che una stalattite");
                    break;
                case 24:
                    Bot.SendTextMessageAsync(id, "Ma @393246337932 dov'è?");
                    break;
                case 25:
                    Bot.SendTextMessageAsync(id, "I polli non sono vere specie, sono cibo che cammina");
                    break;
                case 26:
                    Bot.SendTextMessageAsync(id, "Circle Jerk");
                    break;
                case 27:
                    Bot.SendTextMessageAsync(id, "No! Questo non lo accetto!");
                    break;
                case 28:
                    Bot.SendTextMessageAsync(id, "Ora basta guardare, fammi assaggiare!");
                    break;
                case 29:
                    Bot.SendTextMessageAsync(id, "È come se fossi nudo");
                    break;
                case 30:
                    Bot.SendTextMessageAsync(id, "Lamp oil; rope? Bombs! You want it? It's yours my friend! As long as you have enough Rupees. Sorry Link, I can't give credit! Come back when you're a little.. MMMM... richer!");
                    break;
                case 31:
                    Bot.SendTextMessageAsync(id, "Bruh");
                    break;
                /* */
                default:
                    Bot.SendTextMessageAsync(id, "Non ho ancora abbastanza risposte preimpostate");
                    break;
            }
        }
    }
}
