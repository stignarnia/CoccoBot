using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using System.Text.RegularExpressions;

namespace CoccoBot
{
    class Program
    {
        public static string key = System.IO.File.ReadAllText(@"key.txt").Split('\n')[0];
        public static string[] frasi = System.IO.File.ReadAllText(@"frasi.txt").Split('\n');
        static TelegramBotClient Bot = new TelegramBotClient(key);

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
                        int number = rnd.Next(1, frasi.Length + 1);
                        Invia_Risposta_Preimpostata(number, e.Message.Chat.Id);
                    }
                }

                Random rnd_2 = new Random();
                int prob = rnd_2.Next(1, 201);
                if (prob == 1)
                {
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, frasi[3], replyToMessageId: e.Message.MessageId);
                }
                else if (prob == 2)
                {
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, "Chungastico!", replyToMessageId: e.Message.MessageId);
                }

                if (e.Message.Text.ToLower().Contains("ginevra") && prob <= 100)
                {
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, "Jabba", replyToMessageId: e.Message.MessageId);
                }

                Bind(e);
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

        static void Bind(MessageEventArgs e)
        {
            string[] triggers = System.IO.File.ReadAllText(@"triggers.txt").Split('\n');
            string[] bindings = System.IO.File.ReadAllText(@"bindings.txt").Split('\n');

            for (int i = 0; i < triggers.Length; i++)
            {
                string[] parole = triggers[i].Split(' ');
                foreach (string parola in parole)
                {
                    if (e.Message.Text.ToLower().Contains(parola))
                    {
                        string frase_prob = "";
                        for (int j = 0; j <= i; j++)
                        {
                            frase_prob = bindings[j];
                        }
                        int frase = Int32.Parse(frase_prob.Split(' ')[0]);
                        int prob = Int32.Parse(frase_prob.Split(' ')[1]);
                        Random rnd_3 = new Random();
                        int dice = rnd_3.Next(1, 101);
                        if (dice <= prob)
                        {
                            Invia_Risposta_Preimpostata(frase, e.Message.Chat.Id);
                        }
                    }
                }
            }

        }

        static void Invia_Risposta_Preimpostata(int risposta, long id)
        {
            string frase = "";

            try
            {
                if (risposta == 15)
                {
                    string anno = DateTime.Now.Year.ToString();
                    Bot.SendTextMessageAsync(id, "Siamo nel " + anno + " porca di quella troia");
                }
                else
                {
                    for (int i = 0; i <= risposta; i++)
                    {
                        frase = frasi[i];
                    }
                    Bot.SendTextMessageAsync(id, frase);
                }
            }
            catch
            {
                Bot.SendTextMessageAsync(id, "Non ho ancora abbastanza risposte preimpostate");
            }
        }
    }
}
