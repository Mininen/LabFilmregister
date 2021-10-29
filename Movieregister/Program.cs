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
        public static Movie[] movies = new Movie[0]; 
        static void Main(string[] args)
        {
            if(File.Exists("Filmregister.txt")) 
            {
                ReadFileToArray();
                int choice = 0;

                while(choice != 5) 
                {
                    Console.BackgroundColor = ConsoleColor.Black; 
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("=================================");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("  Welcome to the movie register!  ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("=================================");
                    Console.WriteLine("1.Show movies\n2.Search for movies\n3.Add a movie\n4.Remove a movie\n5.Exit"); 
                    Console.Write("Enter option: ");
                    int.TryParse(Console.ReadLine(), out choice);
                    Console.Clear();

                    if(!(choice >= 1 && choice <= 5)) 
                    {
                        Console.BackgroundColor = ConsoleColor.Red; 
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Enter a number between 1 and 5: ");
                    }

                    if(choice == 1) 
                    {
                        int print = 0;
                        Console.WriteLine("Show movies by:\n1.title\n2.Genre\n3.Year of realese\n4.Length\n5.Score"); 
                        Console.Write("Enter option: ");
                        int.TryParse(Console.ReadLine(), out print); /
                        Console.Clear();

                        switch (print) 
                        {
                            case 1: 
                                SortByTitel();
                                PrintMovies(movies);
                                break;
                            case 2: 
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
                        Console.ForegroundColor = ConsoleColor.DarkGreen; 
                        Console.Write("Press any key to return: ");
                        Console.ReadKey();
                        Console.Clear();
                    }

                    if(choice == 2)  
                    {
                        int search = 0;
                        Console.WriteLine("1.Search by title\n2.Search by genre\n3.Search by score"); 
                        Console.Write("Enter option: ");
                        int.TryParse(Console.ReadLine(), out search);
                        Console.Clear();
                        switch (search) 
                        {
                            case 1: 
                                SearchByTitel();
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.Write("Press any key to return: ");  
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 2:
                                SearchByGenre();
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.Write("Press any key to return: ");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 3:
                                SearchByScore();
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.Write("Press any key to return: ");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            default: 
                                break;
                        }
                    }

                    if (choice == 3) 
                    {
                        Movie newMovie = CreateNewMovie(); 
                        if (newMovie.title != "") 
                        {
                            movies = AddToArray(movies, newMovie); 
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("The movie has been added!\nPress any key to return: ");
                            Console.ReadKey();
                        }
                        Console.Clear();
                    }

                    if (choice == 4) 
                    {
                        try 
                        {
                            if (movies.Length == 0) 
                            {
                                Console.WriteLine("There are no movies to remove!\nPress any key to return");
                                Console.ReadKey();
                                Console.Clear();
                            }
                            else
                            {
                                int index = PickMovieToRemove(movies);
                                movies = RemoveMovieFromArray(movies, index); 
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.Write("Movie nr{0} is now removed!\nPress any key to return: ", index);
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.Clear();
                        }
                    }

                    SaveMoviesToFile();
                }

            }
            else
            {
                Console.WriteLine("The file does not exist.\nPress any key to exit");
                Console.ReadKey();
            }
        }
      // END OF MAIN----------------------------------------------END OF MAIN------------------------------------------------------------END OF MAIN
        
        public static void ReadFileToArray()
        {
            StreamReader infil = new StreamReader("Filmregister.txt");
            string row;
            while((row = infil.ReadLine()) != null) 
            {
                string[] column = row.Split(';');
                Movie movie = new Movie();
                movie.title = column[0];
                movie.year = int.Parse(column[1]);
                movie.genre = column[2];
                movie.length = int.Parse(column[3]);
                movie.score = double.Parse(column[4]);

                movies = AddToArray(movies, movie); 
            }
            infil.Close(); 
        }
     
        public static Movie[] AddToArray(Movie[] oldMovies, Movie newMovie) 
        {
            Movie[] newMovies = new Movie[oldMovies.Length + 1];
            for(int i = 0; i < oldMovies.Length; i++) 
            {
                newMovies[i] = oldMovies[i]; 
            }
            newMovies[newMovies.Length - 1] = newMovie;
            return newMovies; 
        }

        public static void PrintMovies(Movie[] movies)
        {
            Console.WriteLine("Titel:                   Year:    Genre:         Length:    Score:  ");
            for(int i = 0; i < movies.Length; i++)
            {
                PrintOneMovie(movies[i]); 
            }
        }
        public static void PrintOneMovie(Movie movie)
        {
            if(movie.title.Length > 20)
            {
                movie.title = movie.title.Substring(0, 20);
            }
            if(movie.genre.Length > 10)
            {
                movie.genre = movie.genre.Substring(0, 10);
            }
            Console.WriteLine("{0, -20}     {1, 4}     {2, -10}     {3, 3}min     {4, 2}", movie.title, movie.year, movie.genre, movie.length, movie.score);
        }

        public static void Swap(Movie[] movies, int a, int b)
        {
            Movie tempMovie = movies[a];
            movies[a] = movies[b];
            movies[b] = tempMovie;
        }

        public static void SortByTitel()
        {
            for(int i = 0; i < movies.Length; i++)
            {
                int smallest = i;
                for(int j = i + 1; j < movies.Length; j++)
                {
                    if(movies[smallest].title.CompareTo(movies[j].title) > 0)
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

        public static void SearchByTitel()
        {
            Console.Write("Enter movie title: ");
            string search = Console.ReadLine().ToLower();
            Movie[] foundMovies = new Movie[0]; 
            for(int i = 0; i < movies.Length; i++)
            {
                if(movies[i].title.ToLower().Equals(search))
                {
                    foundMovies = AddToArray(foundMovies, movies[i]);
                }
            }
            PrintMovies(foundMovies); 
            if(foundMovies.Length == 0) 
            {
                Console.WriteLine("No movies matching the search was found!");
            }
        }

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

        public static void SearchByScore()
        {
            Console.Write("Enter minimum score: ");
            double search = 0;
            while(!double.TryParse(Console.ReadLine(), out search))
            {
                Console.Write("Enter minimum score: ");
            }
            Movie[] foundMovies = FindScore(search);
            PrintMovies(foundMovies); 
            if(foundMovies.Length == 0)
            {
                Console.WriteLine("No movies matching the search was found!");
            }
        }

        public static Movie[] FindScore(double search) 
        {
            Movie[] foundMovies = new Movie[0];
            for(int i = 0; i < movies.Length; i++)
            {
                if(movies[i].score.Equals(search) || movies[i].score > search) 
                {
                    foundMovies = AddToArray(foundMovies, movies[i]);
                }                      
            }
            return foundMovies;
        }

        public static Movie CreateNewMovie() 
        {
            Movie movie = new Movie(); 
            int year;
            int date;
            int length;
            double score;
            double points;
            Console.Write("Press enter to cancel\nEnter title: ");
            movie.title = Console.ReadLine().Trim(); 
            if (movie.title != "")
            {
                Console.Write("Enter year of release: ");
                int.TryParse(Console.ReadLine().Trim(), out year);
                movie.year = year;
                if (movie.year > DateTime.Now.Year)
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
                if (movie.score > 10)
                {
                    Console.Write("Enter score between 0 - 10: ");
                    double.TryParse(Console.ReadLine().Trim(), out points);
                    movie.score = points;
                }
            }
            return movie;
        }


        public static Movie[] RemoveMovieFromArray(Movie[] currentMovies, int index) 
        {
            Movie[] newMovieArray = new Movie[movies.Length - 1];
            for(int i = 0; i < index - 1; i++) 
            {
                newMovieArray[i] = movies[i];
            }
            for(int i = index; i < movies.Length; i++)
            {
                newMovieArray[i - 1] = movies[i];
            }
            return newMovieArray; 
        }

        public static int PickMovieToRemove(Movie[] currentMovies) 
        {
            PrintMoviesWithIndex(movies);
            Console.Write("Press enter to cancel\nEnter index for movie to remove: ");
            int index = 0;
            int.TryParse(Console.ReadLine(), out index);
            return index; 
        }


        public static void PrintOneMovieWithIndex(Movie movie, int index) 
        {
            if (movie.title.Length > 20) 
            {
                movie.title = movie.title.Substring(0, 20);
            }
            if (movie.genre.Length > 10) 
            {
                movie.genre = movie.genre.Substring(0, 10);
            }
            Console.WriteLine("{0}. {1, -20}     {2, 4}     {3, -10}     {4, 3}min     {5, 2}", index, movie.title, movie.year, movie.genre, movie.length, movie.score);
        }

        public static void PrintMoviesWithIndex(Movie[] movies)
        {
            Console.WriteLine("   Title:                   Year:    Genre:         Length:    Score:  "); 
            for (int i = 0; i < movies.Length; i++)
            {
                PrintOneMovieWithIndex(movies[i], i + 1); 
            }
        }


        public static void SaveMoviesToFile()
        {
            StreamWriter utfil = new StreamWriter("Filmregister.txt");
            for (int i = 0; i < movies.Length; i++)
            {
                Movie movie = movies[i]; 
                utfil.WriteLine("{0};{1};{2};{3};{4}", movie.title, movie.year, movie.genre, movie.length, movie.score); 
            }
            utfil.Close(); 
        }
    }
}
