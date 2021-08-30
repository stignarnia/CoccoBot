using System;
using Telegram.Bot;
using Telegram.Bot.Args;

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
                    string command = e.Message.Text.Replace("@gna_coccobot", "").TrimStart('/');
                    int number;

                    try
                    {
                        number = Int32.Parse(command);
                        Invia_Risposta_Preimpostata(number, e.Message.Chat.Id);
                    }
                    catch
                    {
                        if (command.ToLower() == "random")
                        {
                            Random rnd = new Random();
                            number = rnd.Next(1, frasi.Length);
                            Invia_Risposta_Preimpostata(number, e.Message.Chat.Id);
                        }
                        else if (command.ToLower().StartsWith("addphrase"))
                        {
                            string newPhrase = command.Replace("addphrase", "");
                            newPhrase = newPhrase.Trim();
                            if (newPhrase != "")
                            {
                                AddPhrase(newPhrase, e.Message.Chat.Id);
                            }
                            else
                            {
                                Bot.SendTextMessageAsync(e.Message.Chat.Id, "Aggiungi una frase dopo il comando");
                            }
                        }
                        else if (command.ToLower().Contains("addtrigger"))
                        {
                            string newTrigger = command.Replace("addtrigger", "");
                            newTrigger = newTrigger.Trim();
                            try
                            {
                                string[] trigger_frase_prob = newTrigger.Split(';');
                                newTrigger = trigger_frase_prob[0];
                                string frase = trigger_frase_prob[1].Trim();
                                int prob = Int32.Parse(trigger_frase_prob[2].Trim());
                                if (prob > 0 && prob <= 100)
                                {
                                    AddTrigger(newTrigger, frase, prob, e.Message.Chat.Id);
                                }
                                else
                                {
                                    throw new Exception();
                                }
                            }
                            catch
                            {
                                Bot.SendTextMessageAsync(e.Message.Chat.Id, "Formula il comando in questo modo: " +
                                    "nuovo_trigger_1_senza_spazi nuovo_trigger_2_senza_spazi;" +
                                    " una sola frase da dire anche con spazi che verrà aggiunta al" +
                                    "file da cui pesca random se non c'è già; probabilità_da_1_a_100");
                            }
                        }
                    }
                }

                Random rnd_2 = new Random();
                int prob_2 = rnd_2.Next(1, 201);
                if (prob_2 == 1)
                {
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, frasi[3], replyToMessageId: e.Message.MessageId);
                }
                else if (prob_2 == 2)
                {
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, "Chungastico!", replyToMessageId: e.Message.MessageId);
                }

                if (e.Message.Text.ToLower().Contains("ginevra") && prob_2 <= 100)
                {
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, "Jabba", replyToMessageId: e.Message.MessageId);
                }
                if (e.Message.Text.ToLower().Contains("bot") && (e.Message.Text.ToLower().Contains("bravo") || 
                    e.Message.Text.ToLower().Contains("good")))
                {
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, "Eh ho un algoritmo basato", replyToMessageId: e.Message.MessageId);
                }
                if ((e.Message.Text.ToLower() == "eh" || e.Message.Text.ToLower() == "eh?" ||
                    e.Message.Text.ToLower() == "ehh" || e.Message.Text.ToLower() == "ehh?") && prob_2 <= 100)
                {
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, frasi[2], replyToMessageId: e.Message.MessageId);
                }

                Bind(e);
            }

            if (e.Message.Type.ToString() == "Photo")
            {
                Random rnd_2 = new Random();
                int prob_2 = rnd_2.Next(1, 11);
                if (prob_2 == 1)
                {
                    Invia_Risposta_Preimpostata(17, e.Message.Chat.Id);
                }
            }
        }

        static void AddPhrase(string newPhrase, long id)
        {
            System.IO.File.AppendAllText(@"frasi.txt", Environment.NewLine + newPhrase);
            frasi = System.IO.File.ReadAllText(@"frasi.txt").Split('\n');
            Bot.SendTextMessageAsync(id, "Fatto! Ho aggiunto la frase: " + newPhrase +
                "\nEcco la nuova lista di comandi da mandare a BotFather");
            string commandList = "random - spara una risposta preimpostata a caso" +
                "\naddphrase - aggiunge una risposta preimpostata" +
                "\naddtrigger - aggiunge una o più parole che triggerano una risposta preimpostata";
            for (int i = 1; i < frasi.Length; i++)
            {
                string newLine = "\n" + i + " - " + frasi[i];
                commandList += newLine;
            }
            Bot.SendTextMessageAsync(id, commandList);
        }

        static void AddTrigger(string newTrigger, string frase, int prob, long id)
        {
            int i;

            for (i = 1; i < frasi.Length && !frasi[i].Contains(frase); i++) {; }
            if (i == frasi.Length && !frasi[i-2].Contains(frase))
            {
                AddPhrase(frase, id);
            }
            System.IO.File.AppendAllText(@"triggers.txt", Environment.NewLine + newTrigger);
            System.IO.File.AppendAllText(@"bindings.txt", Environment.NewLine + i + " " + prob);
            Bot.SendTextMessageAsync(id, "Fatto! Ora il " + prob + "% delle volte che vedrò scritte la o le parole -" +
                newTrigger + "- risponderò con " + frasi[i]);
        }

        static void Bind(MessageEventArgs e)
        {
            string[] triggers = System.IO.File.ReadAllText(@"triggers.txt").Split('\n');
            string[] bindings = System.IO.File.ReadAllText(@"bindings.txt").Split('\n');

            for (int i = 0; i < triggers.Length; i++)
            {
                try {
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
                            int prob_3 = Int32.Parse(frase_prob.Split(' ')[1]);

                            Random rnd_3 = new Random();
                            int dice = rnd_3.Next(1, 101);
                            if (dice <= prob_3)
                            {
                                Invia_Risposta_Preimpostata(frase, e.Message.Chat.Id);
                            }
                        }
                    }
                }
                catch
                {
                    return;
                }
            }
        }

        static void Invia_Risposta_Preimpostata(int risposta, long id)
        {
            string frase = "";

            try
            {
                if (risposta == 0)
                {
                    return;
                }
                else if (risposta == 15)
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
