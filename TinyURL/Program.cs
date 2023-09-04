using TinyURL;


URLCodecManager codecManager = new URLCodecManager();

while (true)
{
    Console.WriteLine("-------------------------------\nEnter a URL:");
    string? inputURL = Console.ReadLine();

    if (inputURL is null)
        continue;

    bool searchUrlRes = codecManager.FindURL(inputURL);

    // if URL already exists...
    if (searchUrlRes)
    {
        Console.WriteLine("URL found.");
        bool promptOptionFlag = true;
        while (promptOptionFlag)
        {
            // loop till user inputs correct option
            int option = -1;
            while (option == -1)
            {
                Console.WriteLine("Choose from the following options:\n" +
                    "1) Remove URL from table\n2) Get long URL\n3) Get short URL\n4) Get usage statistics\n5) Exit loop");
                string? optioninput = Console.ReadLine();
                Int32.TryParse(optioninput, out option);
                // reset option if incorrect input
                if (option < 1 || option > 5)
                    option = -1;
            }
            switch (option)
            {
                case 1: codecManager.RemoveURL(inputURL); break;
                case 2:
                    string longUrl = codecManager.GetLongURL(inputURL);
                    Console.WriteLine(longUrl);
                    break;
                case 3:
                    Console.WriteLine(codecManager.ConvertToTinyURL(inputURL));
                    break;
                case 4:
                    if (codecManager.FindURL(inputURL))
                    {
                        int useCount = codecManager.GetUsageStatistics(inputURL);
                        Console.WriteLine(inputURL + "'s longURL has been retrieved " + useCount.ToString() + " times");
                    }
                    break;
                case 5:
                    promptOptionFlag = false;
                    break;
                default: break;
            }
            Console.WriteLine(""); // skip space
        }
    }
    else
    {
        Console.WriteLine("URL is not found. Enter custom tiny URL (if nothing entered a random one will be generated):");
        string? customURL = Console.ReadLine();
        string newURL;
        if (customURL == "")
        {
            codecManager.AddGeneratedURL(inputURL);
            newURL = codecManager.ConvertToTinyURL(inputURL);
        }
        else
        {
            codecManager.AddGeneratedURL(inputURL, customURL);
            newURL = customURL;
        }
        Console.WriteLine("New tinyURL generated: " + newURL);
    }
}

Environment.Exit(0);
