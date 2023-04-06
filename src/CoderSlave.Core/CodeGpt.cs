using OpenAI_API;
using OpenAI_API.Chat;

namespace CoderSlave.Core
{
    public class CodeGpt
    {
        public OpenAIAPI api  { get; set; }
        public Conversation chat { get; set; }
        public CodeGpt(string APIKey,string OrgId)
        {
            api = new OpenAIAPI(new APIAuthentication(APIKey, OrgId));
            Setup();
        }

        async void Setup()
        {
            chat = api.Chat.CreateConversation();

            /// give instruction as System
            chat.AppendSystemMessage("You are a coding assistant that only able to write code in C#. If the user ask to write code, you write a complete code with Program class and the main method without namespace.  You do not say anything else.");

            // give a few examples as user and assistant
            chat.AppendUserInput("Please write a hello world ?");
            chat.AppendExampleChatbotOutput(@"using System;

        public class Program
        {
            public static void Run()
            {
                Console.WriteLine(""Hello World!"");
            }
        }");
            
            chat.AppendUserInput("Please write code to add two number sample ?");
            chat.AppendExampleChatbotOutput(@"using System;

        public class Program
        {
            public static void Run()
            {
                int x = 5;
                int y = 6;
                int sum = x + y;
                Console.WriteLine(sum); // Print the sum of x + y
            }
        }");
            /*
            chat.AppendUserInput("Please write code to create matrix animation ?");
            chat.AppendExampleChatbotOutput(@"using System;

namespace matrix
{
    class Program
    {
        static int Counter;
        static Random rand = new Random();

        static int Interval = 100; // Normal Flowing of Matrix Rain
        static int FullFlow = Interval + 30; // Fast Flowing of Matrix Rain
        static int Blacking = FullFlow + 50; // Displaying the Test Alone

        static ConsoleColor NormalColor = ConsoleColor.DarkGreen;
        static ConsoleColor GlowColor = ConsoleColor.Green;
        static ConsoleColor FancyColor = ConsoleColor.White;
        static String TextInput = ""Matrix"";

        static char AsciiCharacter//Randomised Inputs
        {
            get
            {
                int t = rand.Next(10);
                if (t <= 2)
                    return (char)('0' + rand.Next(10));
                else if (t <= 4)
                    return (char)('a' + rand.Next(27));
                else if (t <= 6)
                    return (char)('A' + rand.Next(27));
                else
                    return (char)(rand.Next(32, 255));
            }
        }
        static void Main()
        {

            Console.ForegroundColor = NormalColor;
            Console.WindowLeft = Console.WindowTop = 0;
            Console.WindowHeight = Console.BufferHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.BufferWidth = Console.LargestWindowWidth;
            Console.SetWindowPosition(0, 0);
            Console.CursorVisible = false;

            int width, height;
            int[] y;
            Initialize(out width, out height, out y);//Setting the Starting Point
           
            while (true)
            {
                Counter = Counter + 1;
                UpdateAllColumns(width, height, y);
                if (Counter > (3 * Interval))
                    Counter = 0;

            }
        }
        private static void UpdateAllColumns(int width, int height, int[] y)
        {
            int x;
            if (Counter < Interval)
            {
                for (x = 0; x < width; ++x)
                {
                    if (x % 10 == 1)//Randomly setting up the White Position
                        Console.ForegroundColor = FancyColor;
                    else
                        Console.ForegroundColor = GlowColor;
                    Console.SetCursorPosition(x, y[x]);
                    Console.Write(AsciiCharacter);

                    if (x % 10 == 9)
                        Console.ForegroundColor = FancyColor;
                    else
                        Console.ForegroundColor = NormalColor;
                    int temp = y[x] - 2;
                    Console.SetCursorPosition(x, inScreenYPosition(temp, height));
                    Console.Write(AsciiCharacter);

                    int temp1 = y[x] - 20;
                    Console.SetCursorPosition(x, inScreenYPosition(temp1, height));
                    Console.Write(' ');
                    y[x] = inScreenYPosition(y[x] + 1, height);
                   
                }
            }
            else if (Counter > Interval && Counter < FullFlow)
            {
                for (x = 0; x < width; ++x)
                {

                    Console.SetCursorPosition(x, y[x]);
                    if (x % 10 == 9)
                        Console.ForegroundColor = FancyColor;
                    else
                        Console.ForegroundColor = NormalColor;

                    Console.Write(AsciiCharacter);//Printing the Character Always at Fixed position

                    y[x] = inScreenYPosition(y[x] + 1, height);
                }
            }
            else if (Counter > FullFlow)
            {
                for (x = 0; x < width; ++x)
                {
                    Console.SetCursorPosition(x, y[x]);
                    Console.Write(' ');//Slowly blacking out the Screen
                    int temp1 = y[x] - 20;
                    Console.SetCursorPosition(x, inScreenYPosition(temp1, height));
                    Console.Write(' ');
                    if (Counter > FullFlow && Counter < Blacking)// Clearing the Entire screen to get the Darkness
                    {
                        if (x % 10 == 9)
                            Console.ForegroundColor = FancyColor;
                        else
                            Console.ForegroundColor = NormalColor;
                        int temp = y[x] - 2;
                        Console.SetCursorPosition(x, inScreenYPosition(temp, height));
                        Console.Write(AsciiCharacter);//The Text is printed Always

                    }
                    Console.SetCursorPosition(width / 2, height / 2);
                    Console.Write(TextInput);
                    y[x] = inScreenYPosition(y[x] + 1, height);
                }

            }
        }
        public static int inScreenYPosition(int yPosition, int height)
        {
            if (yPosition < 0)//When there is negative value
                return yPosition + height;
            else if (yPosition < height)//Normal 
                return yPosition;
            else// When y goes out of screen when autoincremented by 1
                return 0;

        }
        private static void Initialize(out int width, out int height, out int[] y)
        {
            height = Console.WindowHeight;
            width = Console.WindowWidth - 1;
            y = new int[width];
            Console.Clear();

            for (int x = 0; x < width; ++x)//Setting the cursor at random at program startup
            {
                y[x] = rand.Next(height);
            }
        }
    }
}");
            //chat.AppendUserInput("Is this an animal? House");
            //chat.AppendExampleChatbotOutput("No");

            // now let's ask it a question'
            //chat.AppendUserInput("Is this an animal? Dog");
            // and get the response
            //string response = await chat.GetResponseFromChatbotAsync();
            //Console.WriteLine(response); // "Yes"

            // and continue the conversation by asking another
            //chat.AppendUserInput("Is this an animal? Chair");
            // and get another response
            //response = await chat.GetResponseFromChatbotAsync();
            //Console.WriteLine(response); // "No"

            // the entire chat history is available in chat.Messages
           */
        }

        public void ResetConversation()
        {
            Setup();
        }
        public IEnumerable<ConversationChat> GetChatHistory()
        {
            foreach (ChatMessage msg in chat.Messages)
            {
                Console.WriteLine($"{msg.Role}: {msg.Content}");
                yield return new ConversationChat() { Role = msg.Role, Message = msg.Content };

            }
        }
        public async Task<string> Ask(string Prompt)
        {
            try
            {
                // now let's ask it a question'
                chat.AppendUserInput(Prompt);
                // and get the response
                string response = await chat.GetResponseFromChatbotAsync();
                //Console.WriteLine(response); // "Yes"
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return string.Empty;
        }
    }

    public class ConversationChat
    {
        public string Role { get; set; }
        public string Message { get; set; }
    }
}