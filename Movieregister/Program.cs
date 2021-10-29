using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Movieregister
{
    class Program
    {
        public static Movie[] movies = new Movie[0]; // skapar en tom vektor

        static void Main(string[] args)
        {
            if(File.Exists("Filmregister.txt")) // kontroll att textfil existerar
            {
                ReadFileToArray(); // läser in fil till vektor
                int choice = 0;

                while(choice != 5) // loop för programmet
                {
                    Console.BackgroundColor = ConsoleColor.Black; 
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("=================================");
                    Console.BackgroundColor = ConsoleColor.Black; // byter färg på välkomststexten till gult 
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("  Welcome to the movie register!  ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("=================================");
                    Console.WriteLine("1.Show movies\n2.Search for movies\n3.Add a movie\n4.Remove a movie\n5.Exit"); //skriver ut menyval med ny rad
                    Console.Write("Enter option: ");
                    int.TryParse(Console.ReadLine(), out choice); // läser in användarens val och kontrollerar att det är en siffra 
                    Console.Clear(); // rensar skärmen till nästa val så att första menyn försvinner 

                    if(!(choice >= 1 && choice <= 5)) //kotroll att användare skriver in en siffra mellan 1-5
                    {
                        Console.BackgroundColor = ConsoleColor.Red; // ändrar färg till röd bakgrund och vit text
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Enter a number between 1 and 5: ");
                    }

                    if(choice == 1) // skriver filmer på skärmen
                    {
                        int print = 0;
                        Console.WriteLine("Show movies by:\n1.title\n2.Genre\n3.Year of realese\n4.Length\n5.Score"); //meny
                        Console.Write("Enter option: ");
                        int.TryParse(Console.ReadLine(), out print); //tar in val och kontrollerar att det är en siffra
                        Console.Clear();

                        switch (print) // i vilken ordning filmerna ska visas och då används en switch
                        {
                            case 1: // anropar metoden SortByTitel och PrintMovies som sorterar via titel och visar filmerna
                                SortByTitel();
                                PrintMovies(movies);
                                break;
                            case 2: // anropar metoden SortByGenre och PrintMovie som sorterar efter genre och skriver ut filmerna
                                SortByGenre();
                                PrintMovies(movies);
                                break;
                            case 3:
                                SortByYear();
                                PrintMovies(movies);
                                break;
                            case 4:
                                SortBylength();
                                PrintMovies(movies);
                                break;
                            case 5:
                                SortByScore();
                                PrintMovies(movies);
                                break;
                        }
                        Console.BackgroundColor = ConsoleColor.Black; 
                        Console.ForegroundColor = ConsoleColor.DarkGreen; // ändrar färgen till mörkgrön
                        Console.Write("Press any key to return: ");
                        Console.ReadKey();
                        Console.Clear();
                    }

                    if(choice == 2) // söka efter filmer med en meny för sökningsalternativ 
                    {
                        int search = 0;
                        Console.WriteLine("1.Search by title\n2.Search by genre\n3.Search by score"); // meny för söka efter filmen
                        Console.Write("Enter option: ");
                        int.TryParse(Console.ReadLine(), out search);
                        Console.Clear();
                        switch (search) // använder en switch för menyval för sökningar
                        {
                            case 1: // anropar metoden SearchByTitel
                                SearchByTitel();
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.Write("Press any key to return: "); // användaren kan välja valfri tangent att gå tillbaka till menyn med 
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 2: // anropar metoden SearchByGenre
                                SearchByGenre();
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.Write("Press any key to return: ");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 3: // anropar metoden SearchByScore
                                SearchByScore();
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.Write("Press any key to return: ");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            default: // kotroll i fall användaren inte skriver in en siffra mellan 1-3
                                break;
                        }
                    }

                    if (choice == 3) // lägg till en film
                    {
                        Movie newMovie = CreateNewMovie(); // anropar metoden CreateNewMovie och skapar ett nytt Movie objekt "newMovie"
                        if (newMovie.title != "") // kontroll så att det inte läggs till tomma objekt
                        {
                            movies = AddToArray(movies, newMovie); // anropar metoden AddToArray för att lägga till den nya filmen i vektorn
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("The movie has been added!\nPress any key to return: ");
                            Console.ReadKey();
                        }
                        Console.Clear();
                    }

                    if (choice == 4) // ta bort en film
                    {
                        try // undantagshantering
                        {
                            if (movies.Length == 0) // kontroll att programmet inte krashar om det inte finns några filmer i listan
                            {
                                Console.WriteLine("There are no movies to remove!\nPress any key to return");
                                Console.ReadKey();
                                Console.Clear();
                            }
                            else
                            {
                                int index = PickMovieToRemove(movies); // anropar metoden PickMovieToRemove för att få fram index på filmen som ska tas bort  
                                movies = RemoveMovieFromArray(movies, index); // anropar metoden RemoveMovieFromArray som tar bort filmen på det index som metoden ovan retunerade 
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.Write("Movie nr{0} is now removed!\nPress any key to return: ", index);
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                        catch (IndexOutOfRangeException) // catch som fångar upp felet IndexOutOfRangeException
                        {
                            Console.Clear();
                        }
                    }

                    SaveMoviesToFile(); // anropar metoden SaveMoviesToFile för att till sist spara de tillagda filmerna till textfilen
                }

            }
            else // en kontroll som skrivs ut om det inte finns en textfil
            {
                Console.WriteLine("The file does not exist.\nPress any key to exit");
                Console.ReadKey();
            }
        }
      // END OF MAIN----------------------------------------------END OF MAIN------------------------------------------------------------END OF MAIN
        
        // läser in filen till vektorn
        public static void ReadFileToArray()
        {
            StreamReader infil = new StreamReader("Filmregister.txt");
            string row;
            while((row = infil.ReadLine()) != null) // loopen itererar tills en rad är tom/null
            {
                string[] column = row.Split(';');
                Movie movie = new Movie(); // skapar nytt objekt för lägga till filmerna i vektorn
                movie.title = column[0];
                movie.year = int.Parse(column[1]);
                movie.genre = column[2];
                movie.length = int.Parse(column[3]);
                movie.score = double.Parse(column[4]);

                movies = AddToArray(movies, movie); // lägger till filmen i vektorn med hjälp av metoden AddToArray
            }
            infil.Close(); // stänger textfilen
        }
        // lägger till filmer i vektorn
        public static Movie[] AddToArray(Movie[] oldMovies, Movie newMovie) // metoden retunerar en vektor med datatypen Movie och tar in en vektor av typen Movie samt ett objekt av typen Movie
        {
            Movie[] newMovies = new Movie[oldMovies.Length + 1]; // skapar en ny tillfällig vektor som är en plats längre
            for(int i = 0; i < oldMovies.Length; i++) // fyller den tillfälliga vektorn med det som fanns i den gamla vektorn
            {
                newMovies[i] = oldMovies[i]; 
            }
            newMovies[newMovies.Length - 1] = newMovie; // lägger in det nya movie objektet på sista platsen i den tillfälliga vektorn
            return newMovies; // retunerar den nya filmlista
        }

        // skriver ut filmer
        public static void PrintMovies(Movie[] movies) // metoden tar in vektorn med filmer och skriver ut dem
        {
            Console.WriteLine("Titel:                   Year:    Genre:         Length:    Score:  ");
            for(int i = 0; i < movies.Length; i++)
            {
                PrintOneMovie(movies[i]); // anropar metoden PrintOneMovie för att skriva ut filmerna
            }
        }
        // skriver ut en film
        public static void PrintOneMovie(Movie movie) // metoden tar in ett objekt av typen Movie och skriver ut den
        {
            if(movie.title.Length > 20) // kontorll om titeln är längre än 20 tecken skrivs inte alla ut 
            {
                movie.title = movie.title.Substring(0, 20);
            }
            if(movie.genre.Length > 10)
            {
                movie.genre = movie.genre.Substring(0, 10);
            }
            Console.WriteLine("{0, -20}     {1, 4}     {2, -10}     {3, 3}min     {4, 2}", movie.title, movie.year, movie.genre, movie.length, movie.score); // formatering för textplacering
        }

        //Swap metod som byter plats på två objekt
        public static void Swap(Movie[] movies, int a, int b)
        {
            Movie tempMovie = movies[a];
            movies[a] = movies[b];
            movies[b] = tempMovie;
        }

        // sortera via titel 
        public static void SortByTitel()
        {
            for(int i = 0; i < movies.Length; i++)
            {
                int smallest = i;
                for(int j = i + 1; j < movies.Length; j++)
                {
                    if(movies[smallest].title.CompareTo(movies[j].title) > 0) // jämför titeln på två objekt och för att se om det ska bytas plats på
                    {
                        smallest = j;
                    }
                }
                if(i < smallest)
                {
                    Swap(movies, smallest, i); // anropar swap metoden för att byta plats på objekten
                }
            }
        }
        // sortera via genre
        public static void SortByGenre()
        {
            for (int i = 0; i < movies.Length; i++)
            {
                int smallest = i;
                for (int j = i + 1; j < movies.Length; j++)
                {
                    if (movies[smallest].genre.CompareTo(movies[j].genre) > 0)
                    {
                        smallest = j;
                    }
                }
                if (i < smallest)
                {
                    Swap(movies, smallest, i);
                }
            }
        }
        // sortera via score
        public static void SortByYear()
        {
            for(int i = 0; i < movies.Length; i++)
            {
                int smallest = i;
                for(int j = i + 1; j < movies.Length; j++)
                {
                    if(movies[smallest].year < movies[j].year)
                    {
                        smallest = j;
                    }
                }
                if(i < smallest)
                {
                    Swap(movies, smallest, i);
                }
            }
        }
        // sortera via filmens längd
        public static void SortBylength()
        {
            for (int i = 0; i < movies.Length; i++)
            {
                int smallest = i;
                for (int j = i + 1; j < movies.Length; j++)
                {
                    if (movies[smallest].length < movies[j].length)
                    {
                        smallest = j;
                    }
                }
                if (i < smallest)
                {
                    Swap(movies, smallest, i);
                }
            }
        }
        // sortera via betyg
        public static void SortByScore()
        {
            for (int i = 0; i < movies.Length; i++)
            {
                int smallest = i;
                for (int j = i + 1; j < movies.Length; j++)
                {
                    if (movies[smallest].score < movies[j].score)
                    {
                        smallest = j;
                    }
                }
                if (i < smallest)
                {
                    Swap(movies, smallest, i);
                }
            }
        }



        // sökning via titel
        public static void SearchByTitel()
        {
            Console.Write("Enter movie title: ");
            string search = Console.ReadLine().ToLower(); // kontroll att små och stora boktäver inte påverkar sökningen
            Movie[] foundMovies = new Movie[0]; // skapar en ny vektor som ska hålla de hittade filmerna
            for(int i = 0; i < movies.Length; i++)
            {
                if(movies[i].title.ToLower().Equals(search)) //om det finns en film som matchar sökningen så läggs den till i vektorn
                {
                    foundMovies = AddToArray(foundMovies, movies[i]);
                }
            }
            PrintMovies(foundMovies); // anropar metoden PrintMovies som skriver ut de hittade filmerna
            if(foundMovies.Length == 0) // om ingen match hittades skrivs ett meddelande ut på skärmen
            {
                Console.WriteLine("No movies matching the search was found!");
            }
        }
        // sökning via genre, funkar likadant som SearchByTitel
        public static void SearchByGenre()
        {
            Console.Write("Enter movie genre: ");
            string search = Console.ReadLine().ToLower();
            Movie[] foundMovies = new Movie[0];
            for (int i = 0; i < movies.Length; i++)
            {
                if (movies[i].genre.ToLower().Equals(search))
                {
                    foundMovies = AddToArray(foundMovies, movies[i]);
                }
            }
            PrintMovies(foundMovies);
            if (foundMovies.Length == 0)
            {
                Console.WriteLine("No movies matching the search was found!");
            }
        }
        // sökning via lägsta betyg
        public static void SearchByScore()
        {
            Console.Write("Enter minimum score: ");
            double search = 0;
            while(!double.TryParse(Console.ReadLine(), out search))
            {
                Console.Write("Enter minimum score: ");
            }
            Movie[] foundMovies = FindScore(search); // skapar en vektor foundMovies som kommer innehålla hittade filmer
            PrintMovies(foundMovies); // anropar metoden PrintMovies som skirver ut filmerna som matchar sökningen
            if(foundMovies.Length == 0)
            {
                Console.WriteLine("No movies matching the search was found!");
            }
        }
        // hitta betyg
        public static Movie[] FindScore(double search) // metoden ska retunera en vektor med datatypen Movie samt har med sig indata av typen double
        {
            Movie[] foundMovies = new Movie[0]; // skapar en ny vektor för hittade filmer
            for(int i = 0; i < movies.Length; i++)
            {
                if(movies[i].score.Equals(search) || movies[i].score > search) // om det sökta betyget är större eller lika med objektet läggs det till i vektorn för hittade filmer
                {
                    foundMovies = AddToArray(foundMovies, movies[i]);
                }                      
            }
            return foundMovies;
        }

        // skapa ny film
        public static Movie CreateNewMovie() // metoden returnerar ett objekt av typen Movie 
        {
            Movie movie = new Movie(); // skapar ett nytt Movie objekt
            int year;
            int date;
            int length;
            double score;
            double points;
            Console.Write("Press enter to cancel\nEnter title: ");
            movie.title = Console.ReadLine().Trim(); // .Trim tar bort eventuella mellanslag i början och slutet
            if (movie.title != "") // kontroll i fall användaren inte skrvier in någon titel så avbryts tillägningen av film
            {
                Console.Write("Enter year of release: ");
                int.TryParse(Console.ReadLine().Trim(), out year);
                movie.year = year;
                if (movie.year > DateTime.Now.Year) // kontroll att man inte lägger till en film vars utgivningsår inte är större än nuvarande år
                {
                    Console.Write("Enter year of release e.g. 2015: ");
                    int.TryParse(Console.ReadLine().Trim(), out date);
                    movie.year = date;
                }
                Console.Write("Enter genre: ");
                movie.genre = Console.ReadLine().Trim();
                Console.Write("Enter length in minutes: ");
                int.TryParse(Console.ReadLine().Trim(), out length);
                movie.length = length;
                Console.Write("Enter score between 0 - 10: ");
                double.TryParse(Console.ReadLine().Trim(), out score);
                movie.score = score;
                if (movie.score > 10) // kontrollerar att användren inte skriver ett betyg högre än 10
                {
                    Console.Write("Enter score between 0 - 10: ");
                    double.TryParse(Console.ReadLine().Trim(), out points);
                    movie.score = points;
                }
            }
            return movie; // returnerar en ny film 
        }

        // ta bort en film från vektorn
        public static Movie[] RemoveMovieFromArray(Movie[] currentMovies, int index) // metoden returnerar en vektor av typen Movie och har som indata en vektor av typen Movie och en int för index
        {
            Movie[] newMovieArray = new Movie[movies.Length - 1]; // skapar en tillfällig vektor som är en plats mindre 
            for(int i = 0; i < index - 1; i++) // kopierar alla objekt innan index för filmen som ska tas bort
            {
                newMovieArray[i] = movies[i];
            }
            for(int i = index; i < movies.Length; i++) // kopierar alla objekt efter index för filmen som ska tas bort
            {
                newMovieArray[i - 1] = movies[i];
            }
            return newMovieArray; // returnerar den nya film vektorn
        }
        //  val av film som ska tas bort
        public static int PickMovieToRemove(Movie[] currentMovies) // metoden returnerar en int och har som indata en vektor av typen Movie
        {
            PrintMoviesWithIndex(movies); // anropar metoden PrintMoviesWithIndex som skriver ut filmerna med index framför
            Console.Write("Press enter to cancel\nEnter index for movie to remove: ");
            int index = 0;
            int.TryParse(Console.ReadLine(), out index);
            return index; // returnerar index för filmen sóm ska tas bort
        }

        // skriver ut en film med index
        public static void PrintOneMovieWithIndex(Movie movie, int index) // metoden har indata i form av ett objekt av typen Movie och en int för index
        {
            if (movie.title.Length > 20) // kontrollerar längden på titeln
            {
                movie.title = movie.title.Substring(0, 20);
            }
            if (movie.genre.Length > 10) // kontollerar längden på genre
            {
                movie.genre = movie.genre.Substring(0, 10);
            }
            Console.WriteLine("{0}. {1, -20}     {2, 4}     {3, -10}     {4, 3}min     {5, 2}", index, movie.title, movie.year, movie.genre, movie.length, movie.score);
        }
        // skriver ut alla filmer med ett index framför
        public static void PrintMoviesWithIndex(Movie[] movies) // metoden har som indata en vektor av typen Movies
        {
            Console.WriteLine("   Title:                   Year:    Genre:         Length:    Score:  "); 
            for (int i = 0; i < movies.Length; i++)
            {
                PrintOneMovieWithIndex(movies[i], i + 1); // skriver ut index som börjar på 1 framför alla filmer
            }
        }

        // spara till filen
        public static void SaveMoviesToFile()
        {
            StreamWriter utfil = new StreamWriter("Filmregister.txt");
            for (int i = 0; i < movies.Length; i++)
            {
                Movie movie = movies[i]; 
                utfil.WriteLine("{0};{1};{2};{3};{4}", movie.title, movie.year, movie.genre, movie.length, movie.score); // anväder ; istället för tab
            }
            utfil.Close(); // stänger textfilen
        }
    }
}
